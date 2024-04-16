using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSys.Business_Layers.User
{
    class UserFunction
    {
        static Reservation_SysEntities1 RRS = new Reservation_SysEntities1();
        Train_Details td = new Train_Details();
        static User_details ud = new User_details();
        static Booked_Ticket bt = new Booked_Ticket();
        static int uid;


        public static void User_Login()
        {
            Console.WriteLine();
            Console.WriteLine("\t1. Existing User Press '1'");
            Console.WriteLine("\t2. New User Press '2'");
            Console.WriteLine("\t3.For Exiting From the Console App Press '3'");
            Console.Write("Your Choice : ");
            int x = Convert.ToInt32(Console.ReadLine());

            if (x == 1)
            {
                Console.Clear();
                Console.WriteLine("=======================================================================");
                Console.WriteLine("\t---Welcome User---");
                Console.WriteLine("=======================================================================");
                
                Validate_User();
            }
            else if (x == 2)
            {
                User_details();
                User_Option();
            }
            else if (x == 3)
            {
                Console.Clear();
                Program.ReservationPortal();
            }

        }

        //what user can do with his level of permission
        static void User_Option()
        {
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine("\t----Ticket Booking and cancellation Portal----");
            Console.WriteLine("====================================================================");
            Console.WriteLine("\t1. Book Ticket Press '1'");
            Console.WriteLine("\t2. Show Booked Ticket Press '2'");
            Console.WriteLine("\t3. Cancel Ticket Press '3'");
            Console.WriteLine("\t4. Refund History Press '4'");
            Console.WriteLine("\t5. Exit... Press '5'");
            Console.Write("Your Choice : ");
            int inst = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("----------------------------------------------------------");
            Console.Clear();
            Console.WriteLine();



            if (inst == 1)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Welcome to Ticket Booking Portal---");
                Console.WriteLine("===================================================================================");
                Show_Train();
            start:
                Console.Write("\nEnter Train Number of Train you want to book ticket for:");
               
                int trainno = int.Parse(Console.ReadLine());
                var train = RRS.Train_Details.Where(t => t.Train_No == trainno && t.Train_Status == "Active").FirstOrDefault();
                if (train != null)
                {
                    ShowFare_Seat(trainno);
                    BookTicket(uid, trainno);
                }
                else
                {
                    Console.WriteLine("Select From Available Trains.....");
                    goto start;
                }

            }
            else if (inst == 2)
            {
                Console.Clear();
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Booked Ticket---");
                Console.WriteLine("===================================================================================");
                ShowBookedTicket(uid);
                Console.WriteLine("Press Tab\\Enter to Continue.....");
                Console.ReadKey();
                Console.Clear();
                User_Option();

            }
            else if (inst == 3)
            {
                Console.Clear();
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Cancel Ticket---");
                Console.WriteLine("===================================================================================");
                ShowBookedTicket1(uid);
                Random r = new Random();
                int cid = r.Next(1111, 9999);
   
                Console.Write("\tEnter Your PNR Number to cancel: ");
                int pnrno = int.Parse(Console.ReadLine());

                RRS.CancelTicket(cid, pnrno);//Cancel ticket proc
                RRS.cancelBooking(pnrno);//cancel booking proc
                int trainNo = (int)RRS.Booked_Ticket.Where(t => t.PNR_No == pnrno).Select(t => t.Train_No).FirstOrDefault();
                string @class = (string)RRS.Booked_Ticket.Where(t => t.PNR_No == pnrno).Select(t => t.Status).FirstOrDefault();
                int seat = (int)RRS.Booked_Ticket.Where(bt => bt.PNR_No == pnrno).Select(bt => bt.No_of_Seats).FirstOrDefault();
                RRS.SeatManageProcCancel(trainNo, @class, seat);

                Console.WriteLine("Cancellation Successful......");
                Console.WriteLine("Press Tab\\Enter to continue.....");
                Console.ReadKey();
                Console.Clear();
                User_Option();

            }
            else if (inst == 4)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Refund History---");
                Console.WriteLine("===================================================================================");
                Console.WriteLine();
                refund_History(uid);
                Console.WriteLine();
                Console.WriteLine("Press Tab\\Enter to continue.....");
                Console.ReadKey();
                Console.Clear();
                User_Option();
            }
            else if (inst == 5)
            {
                Console.Clear();
                Program.ReservationPortal();
            }
            else
            {
                Console.WriteLine("Please Choose a valid option");
                Console.Clear();
                User_Option();
            }

        }

        //for Diplaying all trains
        public static void Show_Train()
        {
            Console.WriteLine("===================================================================================");
            Console.WriteLine("\t--- Available Trains---");
            Console.WriteLine("===================================================================================");
            var trains = RRS.Train_Details.Where(t => t.Train_Status == "Active");
            int cnt = 1;
            Console.WriteLine($"->\tTrain-No\t\tTrain-Name\t\tSource\t\tDestination");

            foreach (var train in trains)
            {
                Console.WriteLine("----------------------------------------------------------------------------------");
                Console.WriteLine($"{cnt}\t{train.Train_No}\t\t\t{train.Train_Name}\t\t{train.Source}\t{train.Destination}");
                cnt++;
                Console.WriteLine("----------------------------------------------------------------------------------");
            }
            
        }



        //for ticket booking
        public static void BookTicket(int uid, int trainno)
        {
            Console.WriteLine("===================================================");
            Console.WriteLine("\t----Ticket Booking Portal----");
            Console.WriteLine("===================================================");
            Random r = new Random();
            int pnr = r.Next(11111, 99999);
            bt.PNR_No = pnr;
            bt.User_id = uid;
            bt.Train_No = trainno;
            Console.Write("Enter the Number of Passenger: ");
            int numofpass = int.Parse(Console.ReadLine());
            
            if (numofpass < 6)
            {
                bt.No_of_Seats = numofpass;
                Console.WriteLine("---------------------------------");
            start:       //defining go to label...
                Console.Write("\tFor First class Enter '1AC'\n\tSecond Class '2AC'\n\tThird Class '3AC'\n\tSleeper Class 'SL'\nYour Choice:");
                String input = Console.ReadLine().ToUpper();
                bt.Ticket_Class = input;
                double totfare = 0;
                switch (input)
                {
                    case "1AC":
                        totfare = numofpass * CalcFare(trainno, "First");
                        break;
                    case "2AC":
                        totfare = numofpass * CalcFare(trainno, "Second");
                        break;
                    case "3AC":
                        totfare = numofpass * CalcFare(trainno, "Third");
                        break;
                    case "SL":
                        totfare = numofpass * CalcFare(trainno, "Sleeper");
                        break;
                    default:
                        Console.WriteLine("Please Choose a Valid option");
                        goto start;
                }

                bt.TotalFare = totfare + 70;
                bt.Booking_Date_Time = DateTime.Now;
                bt.Status = "Confirm";
                RRS.Booked_Ticket.Add(bt);
                RRS.SaveChanges();

                //for number of ticket
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Enter Passenger Details....");
                for (int i = 0; i < numofpass; i++)
                {
                    Console.WriteLine("----------------------------------");
                    int pid = r.Next(111, 999);
                    Console.Write("Enter Passenger Name: ");
                    string pname = Console.ReadLine();
                    //@pid,@uid,@pname,@Age parameters names in database
                    Console.Write("Passenger Age: ");
                    int age = int.Parse(Console.ReadLine());
                    RRS.AddPassenger(pid, pnr, pname, age);
                    RRS.SeatManageProc(trainno, input);
                    Console.WriteLine("----------------------------------");
                }
                Console.WriteLine("\tBooking Confirm...");
                Console.WriteLine();
                Console.WriteLine("************************************************************");
                Console.WriteLine("---------------------");
                Console.WriteLine("<<<Booking Details>>>");
                Console.WriteLine("---------------------");
                Console.WriteLine($"\tPNR No: {pnr}\tUser-Id: {uid}\tTrain No: {trainno}\n" +
                    $"\tNumber of Seats Booked: {numofpass}\tTotal Fare: {totfare}\t\t Status: Confirmed... ");
                Console.WriteLine("************************************************************");
                RRS.SeatManageProc(trainno, input);   // calling procedure
                Console.WriteLine();
                Console.Write("Press Tab\\Enter to Continue...");
                Console.ReadKey();
                Console.Clear();
                User_Option();
            }
            else
            {
                Console.WriteLine("Maximum number of tickets allowed at a time is 5......");
                Console.WriteLine();
                Console.Clear();
                BookTicket(uid,trainno);
            }
           
        }



        //calculate fair
        static double CalcFare(int trno, string cls)
        {
            var totalFare = RRS.Class_Fare.Where(f => f.Train_No == trno).Select(f => f.C1_A).FirstOrDefault();
            if (cls == "Second")
                totalFare = RRS.Class_Fare.Where(f => f.Train_No == trno).Select(f => f.C2_A).FirstOrDefault();
            else if (cls == "Third")
                totalFare = RRS.Class_Fare.Where(f => f.Train_No == trno).Select(f => f.C3_A).FirstOrDefault();
            else if (cls == "Sleeper")
                totalFare = RRS.Class_Fare.Where(f => f.Train_No == trno).Select(f => f.SL).FirstOrDefault();

            return totalFare;
        }



        //for showing the existing booked ticket for the user
        static void ShowBookedTicket(int uid)
        {
            Console.WriteLine("===================================================================================");
            Console.WriteLine("\t---Booking Information---");
            Console.WriteLine("===================================================================================");
            var booked_tkt = RRS.Booked_Ticket.Where(bt => bt.User_id == uid);

            if (booked_tkt != null)
            {
                foreach (var bt in booked_tkt)
                {
                    Console.WriteLine("\n----------------------------------------------------------------------");
                    Console.WriteLine($"\t|PNR No: {bt.PNR_No} | \t|Train No: {bt.Train_No} | \t|Booking Date&Time :{bt.Booking_Date_Time}|\n" +
                        $"\t|Source: {bt.Train_Details.Source} | \t|Destination: {bt.Train_Details.Destination} |\t|Number of Passenger: {bt.No_of_Seats}| ");

                    Console.WriteLine($"Total Fare: {bt.TotalFare}Rs.\tStatus: {bt.Status}");
                    Console.WriteLine("70-Rs Extra for platform Charges..");
                    Console.WriteLine("----------------------------------------------------------------------");
                }
                Console.Write("Do You Want to see Additional information Y?N: ");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {
                    Console.Write("Enter the PNR-No: ");
                    int pnr1 = int.Parse(Console.ReadLine());
                    var passenger = RRS.Passengers.Where(p => p.PNR_No == pnr1).ToList();
                    foreach (var p in passenger)
                    {
                        Console.WriteLine("----------------------------------------------------------------------");
                        Console.WriteLine($"Passenger ID: {p.P_Id} | \tName: {p.P_Name} | \tAge: {p.P_Age} |");
                        Console.WriteLine("----------------------------------------------------------------------");
                    }

                }
                else
                    User_Option();
                Console.WriteLine("Press Tab\\Enter to Continue...");
                Console.ReadKey();
                User_Option();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No Booking Data Found......");
                Console.WriteLine();
                Console.WriteLine("Press Tab\\Enter to Continue...");
                Console.ReadKey();
                User_Option();
            }
        }



        //for getting the and seeting the user details into database
        static void User_details()
        {
            Console.Clear();
            Console.WriteLine("===================================================================================");
            Console.WriteLine("\t---User Registration Portal---");
            Console.WriteLine("===================================================================================");
            Console.WriteLine();
            Console.Write("\tEnter User-id : ");
            uid = int.Parse(Console.ReadLine());
            ud.User_id = uid;
       
            Console.Write("\n\n\tEnter Name : ");
            ud.User_Name = Console.ReadLine();
         
            Console.Write("\n\n\tEnter Age : ");
            ud.Age = int.Parse(Console.ReadLine());
      
            Console.Write("\n\n\tEnter Passcode : ");
            ud.Passcode = Console.ReadLine();
            RRS.User_details.Add(ud);
            RRS.SaveChanges();
            Console.WriteLine("");
            Console.WriteLine("Registration Successful....\n");
            Console.WriteLine("Press Tab\\Enter to Continue...");
            Console.ReadKey();  
        }





        //showing avl seats and price for different classes
        static void ShowFare_Seat(int tno)
        {
            var fare = RRS.Class_Fare.Where(t => t.Train_No == tno).SingleOrDefault();
            var seat = RRS.Seat_Availability.Where(s => s.Train_No == tno).SingleOrDefault();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("\n---Prices and Available Seats for Different Train Classes---");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("\t You have Select Train : " + tno);
            Console.WriteLine($"Train No: {fare.Train_No}  \tFirstAC-> Price: {fare.C1_A} | \tSeats: {seat.C1_A}\n" +
                $"\tSecondAC-> Price: {fare.C2_A} | Seats: {seat.C2_A}\n" +
                $"\tThirdAc Price: {fare.C3_A} | \tSeats: {seat.C3_A}|\n" +
                $"\tSL Price: {fare.SL} | \tSeats: {seat.SL}|");

        }


        //validating id and pass while log in
        static void Validate_User()
        {
            Console.Clear();
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\tUser Authentication..");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.Write("\tEnter User-ID: ");
            uid = int.Parse(Console.ReadLine());
            Console.Write("\tEnter User Password: ");
            string pass = Admin.AdminFunction.ReadPassword();
            var validate = Validate(uid, pass);

            if (validate)
            {
                User_Option();
            }
            else
            {
                Console.WriteLine("Invalid User-id or Password \n--------Try Again------");
                Console.Clear();
                User_Login();
            }
        }
        private static bool Validate(int user_id, string passcode)
        {
            var user = RRS.User_details.FirstOrDefault(u => u.User_id == user_id && u.Passcode == passcode);
            return user != null;
        }
        //public static void CancelledTicket(int pnr)
        //{
        //    Random r = new Random();
        //    int cid = r.Next(11111, 99999);
        //    var x = RRS.Booked_Ticket.Find(pnr);
        //    if (x != null)
        //    {

        //    }
        //    else
        //    {
        //        Console.WriteLine("\nNo Booking ID Found....");
        //        User_Option();
        //    }
        //}
        static void refund_History(int uid)
        {
            var refund = RRS.Canceled_Ticket.Where(r => r.User_id == uid).ToList();
            if (refund != null)
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("\t Refund History...");
                Console.WriteLine("----------------------------------------------------------------------");
                foreach (var v in refund)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine($"\tCancel-Id: {v.Canceled_id}  \tPNR-No: {v.PNR_No}\tUser-id: {v.User_id}\t\nrain-No: {v.Train_No}\tCancel-Date & Time: {v.Cancellation_Date_Time}\t\tRefund:{v.Refund_Ammount}");
                    Console.WriteLine("----------------------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("No Data Found.....");
                Console.WriteLine();
                Console.WriteLine("Press Tab\\Enter To continue....");
                Console.ReadKey();
                Console.Clear();
                User_Option();
            }

        }
        static void ShowBookedTicket1(int uid)
        {
            
            var booked_tkt = RRS.Booked_Ticket.Where(bt => bt.User_id == uid);
            if (booked_tkt.Any())
            {
                foreach (var bt in booked_tkt)
                {
                    Console.WriteLine();
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("\n---Booked Ticket---");
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine("\n----------------------------------------------------------------------");
                    Console.WriteLine($"\t|PNR No: {bt.PNR_No} | \t|Train No: {bt.Train_No} | \t|Booking Date&Time :{bt.Booking_Date_Time}|\n" +
                        $"\t|Source: {bt.Train_Details.Source} | \t|Destination: {bt.Train_Details.Destination} |\t|Number of Passenger: {bt.No_of_Seats}| ");

                    Console.WriteLine($"Total Fare: {bt.TotalFare}Rs.\tStatus: {bt.Status}");
                    Console.WriteLine("70-Rs Extra for platform Charges..");
                    Console.WriteLine("----------------------------------------------------------------------");
                }
            }
            else
            {
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("No Booking found...");
                Console.WriteLine("----------------------------------------------------------------------");
                Console.WriteLine("Press Tab\\Enter to Continue....");
                Console.ReadKey();
                User_Option();
            }
            

        }
    }
} 
