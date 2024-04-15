using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservationSys.Business_Layers.Admin
{
    class AdminFunction
    {
        static Reservation_SysEntities1 RRS = new Reservation_SysEntities1();
        static Train_Details td = new Train_Details();
    
        public static void Admin_Login()
        {
            Console.WriteLine("\t---Welcome Admin---");
            Console.WriteLine("===================================================================================");
            Validate_Admin();
        }
        static void Validate_Admin()
        {
            //for validating the existing user
            Console.Write("Enter Admin-ID: ");
            int uid = int.Parse(Console.ReadLine());
            Console.Write("Enter Admin Password: ");
            string pass = Console.ReadLine();
            var validate = Validate(uid, pass);

            if (validate)
            {
                Console.WriteLine("---Welcome Admin---");
                AdminOption();
            }
            else
            {
                Console.WriteLine("Invalid Admin-id or Password \n--------Try Again------");
                Validate_Admin();
            }
        }
        private static bool Validate(int admin_id, string passcode)
        {
            Admin_Details ad = new Admin_Details();
            var admin = RRS.Admin_Details.FirstOrDefault(a => a.Admin_id.Equals(admin_id) && a.passcode.Equals(passcode));
            return admin != null;
        }
        static void AdminOption()
        {
            Console.WriteLine("===================================================================================");
            Console.WriteLine("\t------Welcome To Admin Portal------");
            Console.WriteLine("===================================================================================");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\t1. Add Train Press '1'");
            Console.WriteLine("\t2. Modify Train Press '2'");
            Console.WriteLine("\t3. Delete Train Press '3'");
            Console.WriteLine("\t4. Exit... Press '4'");
            Console.Write("Your Choice: ");
            int inst = int.Parse(Console.ReadLine());
            Console.WriteLine("\n---------------------------------------------");

            if (inst == 1)
            {
                //Add Train
                Add_Train();
            }
            else if (inst == 2)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t------Train Modification Portal------");
                Console.WriteLine("===================================================================================");
                UpdateTrainName();
            }
            else if (inst == 3)
            {
                //delete train

                Console.WriteLine("\tFor Permanently Delete Press '1");
                Console.WriteLine("\tFor Activate the Train Press '2'");
                Console.WriteLine("\tFor Deactivate The train Press '3'");
                Console.WriteLine("your Choice: ");
                start:
                int ans = int.Parse(Console.ReadLine());
                if (ans == 1)
                    Delete_Train();
                else if (ans == 2)
                    ActivateTrain();
                else if (ans == 3)
                    DeActivateTrain();
                else
                {
                    Console.WriteLine("Wrong Choice Try Again.....");
                    goto start;
                }
                    

            }
            else if (inst == 4)
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Please Choose a valid option....");
                AdminOption();
            }
                
        }
        //add train into data table
        static void Add_Train()
        {
            Console.WriteLine("=======================================================================");
            Console.WriteLine("\tAdd Train Portal.....");
            Console.WriteLine("=======================================================================");
            Console.Write("Enter Train No: ");
            int trno = int.Parse(Console.ReadLine());
            td.Train_No = trno;
            Console.Write("Enter Train Name: ");
            td.Train_Name = Console.ReadLine();
            Console.Write("Enter Source: ");
            td.Source = Console.ReadLine();
            Console.Write("Enter Destination: ");
            td.Destination = Console.ReadLine();
            td.Train_Status = "Active";
            RRS.Train_Details.Add(td);
            RRS.SaveChanges();
            Show_Train();
            Console.ReadKey();
            Console.WriteLine("\t---Train has been added sucessFully---");

            //adding seats and fare details..
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("Now add Train Seats and fare according to Class...");
            Console.Write("Enter 1-AC Seats: ");
            int FristACSeat = int.Parse(Console.ReadLine());
            Console.Write("\nEnter 2-AC Seats: ");
            int ScdACSeat = int.Parse(Console.ReadLine());
            Console.Write("\nEnter 3-AC Seats: ");
            int ThirdACSeat = int.Parse(Console.ReadLine());
            Console.Write("\nEnter SL Seats: ");
            int SLSeat = int.Parse(Console.ReadLine());
            RRS.Add_Fare(trno, FristACSeat, ScdACSeat, ThirdACSeat, SLSeat);//train seat proc
            Console.Write("Enter 1-AC Fare: ");
            int FirstACFare = int.Parse(Console.ReadLine());
            Console.Write("Enter 2-AC Seats: ");
            int ScdACFare  = int.Parse(Console.ReadLine());
            Console.Write("Enter 3-AC Seats: ");
            int ThirdACFare = int.Parse(Console.ReadLine());
            Console.Write("Enter SL Seats: ");
            int SLFare = int.Parse(Console.ReadLine());
            RRS.Add_Seat(trno, FirstACFare, ScdACFare, ThirdACFare, SLFare); //train fare proc
            Console.WriteLine("Train Details has been added successfully...");
            Console.WriteLine("---------------------------------------------");
            Console.Read();
            AdminOption();
        }
        //delete train from data table
        static void Delete_Train()
        {
            Show_Train();
            Console.Write("Enter Train No you want to delete :");
            int trainno = int.Parse(Console.ReadLine());
            var TrainToRemove = RRS.Train_Details.SingleOrDefault(t => t.Train_No == trainno);
            if (TrainToRemove != null)
            {
                Console.Write("\nAre you sure you want to delete the Train 'Y/N': ");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {
                    RRS.Train_Details.Remove(TrainToRemove);
                    RRS.SaveChanges();
                    Console.WriteLine("\n---Sucessfully Deleted---");
                    Show_Train();
                    AdminOption();
                }
                else
                    AdminOption();
            }
            else
            {
                Console.WriteLine("\n ---Train can't be found please select a try form the list---");
                AdminOption();
            }

        }

        //update data into data table
        static void UpdateTrainName()
        {
            Show_Train();
            Console.Write("\nEnter the train No you Want to Modify:");
            int tno = int.Parse(Console.ReadLine());
            var modified = RRS.Train_Details.FirstOrDefault(t => t.Train_No == tno);

            if (modified != null)
            {
                Console.Write("You Sure you want to update Y/N: ");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {
                    Console.WriteLine($"Enter Train New Name For Train No: {tno} : ");
                    string tname = Console.ReadLine();
                    modified.Train_Name = tname;
                    RRS.SaveChanges();
                    Console.WriteLine("Train data has been modified");
                    Show_Train();
                }
                else
                    AdminOption();
            }
            else
            {
                Console.WriteLine("----Selected Train can't be Found----");
                AdminOption();
            }

        }
        //method for deactivate the train
        static void DeActivateTrain()
        {
            Show_Train();
            Console.WriteLine("Enter The Train No which you want to Deactivate");
            int trainno = int.Parse(Console.ReadLine());
            var status = RRS.Train_Details.FirstOrDefault(t => t.Train_No == trainno);
            if (status != null)
            {
                Console.Write("You sure You want to deactivate the train Y/N: ");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {
                    status.Train_Status = "DeActive";
                    RRS.SaveChanges();
                    Console.WriteLine("Train data has been modified");
                }
                else
                    AdminOption();
            }
            else
            {
                Console.WriteLine("Enter a Valid Train No...");
                DeActivateTrain();
            }

        }
        static void ActivateTrain()
        {
            Show_Train();
            Console.WriteLine("Enter The Train No which you want to Activate");
            int trainno = int.Parse(Console.ReadLine());
            var status = RRS.Train_Details.FirstOrDefault(t => t.Train_No == trainno);
            if (status != null)
            {
                Console.Write("You sure You want to Activate the train Y/N: ");
                string ans = Console.ReadLine().ToUpper();
                if (ans == "Y")
                {
                    status.Train_Status = "Active";
                    RRS.SaveChanges();
                    Console.WriteLine("Train data has been modified");
                }
                else
                    AdminOption();
            }
            else
            {
                Console.WriteLine("Enter a Valid Train No...");
                ActivateTrain();
            }

        }
        static void Show_Train()
        {
            Console.WriteLine("\n---Train Details---");
            var trains = RRS.Train_Details.ToList();
            int cnt = 1;
            Console.WriteLine($"->\tTrain-No\t\tTrain-Name\t\tSource\t\tDestination\t\tStatus");
            foreach (var train in trains)
            {
                Console.WriteLine($"{cnt}\t{train.Train_No}\t\t\t{train.Train_Name}\t\t{train.Source}\t{train.Destination}\t\t{train.Train_Status}");
                cnt++;
            }
        }
    }
}
