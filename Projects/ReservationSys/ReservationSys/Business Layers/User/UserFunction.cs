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
            Console.WriteLine("====================================================================");
            Console.WriteLine("\tExisting User Press '1'");
            Console.WriteLine("\tNew User Press '2'");
            Console.WriteLine("\tFor Exiting From the Console App Press '3'");
            Console.Write("Your Choice : ");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("===================================================");

            if (x == 1)
            {
                Console.WriteLine("\t---Welcome User---");
                Console.WriteLine("===================================================");
                Validate_User();
            }
            else if (x == 2)
            {
                User_details();
                User_Option();
            }
            else if (x == 3)
            {
                Environment.Exit(0);
            }

        }

        //what user can do with his level of permission
        static void User_Option()
        {
            start:
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
            Console.WriteLine();



            if (inst == 1)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Welcome to Ticket Booking Portal---");
                Console.WriteLine("===================================================================================");
                Show_Train();
                Console.Write("\nEnter Train Number of Train you want to book ticket for:");
                int trainno = int.Parse(Console.ReadLine());
                ShowFare_Seat(trainno);
                BookTicket(uid, trainno);

            }
            else if (inst == 2)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Booked Ticket---");
                ShowBookedTicket(uid);

                Console.Read();
                User_Option();

            }
            else if (inst == 3)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t---Cancel Ticket---");
                Console.WriteLine("===================================================================================");
                Random r = new Random();
                int cid = r.Next(1111, 9999);
                ShowBookedTicket(uid);
                Console.Write("\tEnter Your PNR Number to cancel: ");
                int pnrno = int.Parse(Console.ReadLine());

                RRS.CancelTicket(cid, pnrno);//Cancel ticket proc
                RRS.cancelBooking(pnrno);//cancel booking proc
                int trainNo = (int)RRS.Booked_Ticket.Where(t => t.PNR_No == pnrno).Select(t => t.Train_No).FirstOrDefault();
                string  @class= (string)RRS.Booked_Ticket.Where(t => t.PNR_No == pnrno).Select(t => t.Status).FirstOrDefault();
                int seat = (int)RRS.Passengers.Where(t => t.PNR_No == pnrno).Count();
                RRS.SeatManageProcCancel(trainNo, @class, seat);

            }
            else if (inst == 4)
            {
                refund_History(uid);
                Console.ReadKey();
                User_Option();
            }
            else if (inst == 5)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please Choose a valid option");
                goto start;
            }
                
        }

        //for Diplaying all trains
        public static void Show_Train()
        {
            Console.WriteLine("=================================================================================================");
            Console.WriteLine("\n\t---Train Details---");
            var trains = RRS.Train_Details.Where(t => t.Train_Status == "Active");
            int cnt = 1;
            Console.WriteLine($"->\tTrain-No\t\tTrain-Name\t\tSource\t\tDestination");
            
            foreach (var train in trains)
            {
                Console.WriteLine("---------------------------------------------------------------------------------------------");
                Console.WriteLine($"{cnt}\t{train.Train_No}\t\t\t{train.Train_Name}\t\t{train.Source}\t{train.Destination}");
                cnt++;
                Console.WriteLine("---------------------------------------------------------------------------------------------");
            }
            Console.WriteLine("=================================================================================================");
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
            Console.Write("Enter the Number of Passenger:");
            Console.WriteLine("---------------------------------");
            int numofpass = int.Parse(Console.ReadLine());
           
            start:       //defining go to label...
            Console.Write("For First class Enter '1AC'\nSecond Class '2AC'\nThird Class '3AC'\nSleeper Class 'SL'\nYour Choice:");
            String input = Console.ReadLine().ToUpper();
            bt.Ticket_Class = input;
            double totfare = 0;
            switch (input)
            {
                case "1AC":
                    totfare = numofpass*CalcFare(trainno, "First");
                    break;
                case "2AC":
                    totfare = numofpass*CalcFare(trainno, "Second");
                    break;
                case "3AC":
                    totfare = numofpass * CalcFare(trainno, "Third");
                    break;
                case "SL":
                    totfare =numofpass * CalcFare(trainno, "Sleeper");
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
            Console.WriteLine("===================================================");
            RRS.SeatManageProc(trainno,input);   // calling procedure
            User_Option();
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
            var booked_tkt = RRS.Booked_Ticket.Where(bt => bt.User_id == uid);
           
            foreach (var bt in booked_tkt)
            {
                Console.WriteLine("\n----------------------------------------------------------------------");
                Console.WriteLine($"PNR No: {bt.PNR_No} | \tTrain No: {bt.Train_No} | \tBooking Date&Time :{bt.Booking_Date_Time}\n" +
                    $"\tSource: {bt.Train_Details.Source} | \tDestination: {bt.Train_Details.Destination} | ");
                
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
                foreach(var p in passenger)
                {
                    Console.WriteLine("----------------------------------------------------------------------");
                    Console.WriteLine($"Passenger ID: {p.P_Id} | \tName: {p.P_Name} | \tAge: {p.P_Age} |");
                    Console.WriteLine("----------------------------------------------------------------------");
                }
               
            }

            Console.Read();
            Console.WriteLine("===================================================================================");
            User_Option();
        }



        //for getting the and seeting the user details into database
        static void User_details()
        {
            Console.WriteLine("===================================================================================");
            Console.WriteLine();
            Console.Write("Enter User-id : ");
            uid = int.Parse(Console.ReadLine());
            ud.User_id = uid;
            Console.Write("\nEnter Name : ");
            ud.User_Name = Console.ReadLine();
            Console.Write("\nEnter Age : ");
            ud.Age = int.Parse(Console.ReadLine());
            Console.Write("\nEnter Passcode : ");
            ud.Passcode = Console.ReadLine();
            RRS.User_details.Add(ud);
            RRS.SaveChanges();
            Console.WriteLine("\n===================================================================================");
        }



       

        //showing avl seats and price for different classes
        static void ShowFare_Seat(int tno)
        {
            var fare = RRS.Class_Fare.Where(t => t.Train_No == tno).SingleOrDefault();
            var seat = RRS.Seat_Availability.Where(s => s.Train_No == tno).SingleOrDefault();
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("\n---Prices and Available Seats for Different Train Classes---");
            Console.WriteLine("----------------------------------------------------------------------");
            Console.WriteLine("\t You have Select Train : "+tno);
            Console.WriteLine("Train No | \tFirstAC | \tSeats | \t\tSecondACSeats | \t\tThirdAc | \tSeats |\t\tSL | \tSeats|");

            Console.WriteLine($"{fare.Train_No}\t\t{fare.C1_A}Rs\t{seat.C1_A}\t\t{fare.C2_A}Rs\t{seat.C2_A} \t\t{fare.C3_A}Rs\t{seat.C3_A}\t\t{fare.SL}Rs\t{seat.SL}");

        }
        //validating id and pass while log in
        static void Validate_User()
        {
            //for validating the existing user
            Console.Write("Enter User-ID: ");
            uid = int.Parse(Console.ReadLine());
            Console.Write("Enter User Password: ");
            string pass = Console.ReadLine();
            var validate = Validate(uid, pass);

            if (validate)
            {
                User_Option();
            }
            else
            {
                Console.WriteLine("Invalid User-id or Password \n--------Try Again------");
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
            Console.WriteLine("\tCancel-Id\tPNR-No\tUser-id\tTrain-No\tCancel-Date & Time\tRefund");
            foreach(var v in refund)
            {
                Console.WriteLine($"\t{v.Canceled_id}  \t{v.PNR_No}\t{v.User_id}\t{v.Train_No}\t{v.Cancellation_Date_Time}\t{v.Refund_Ammount}");
            }
        }
    }
}
