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
            Validate_Admin();
        }
        static void Validate_Admin()
        {
            //for validating the existing user
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine("\tAdmin Authentication..");
            Console.WriteLine("---------------------------------------------");
            Console.WriteLine();
            Console.Write("\tEnter Admin-ID: ");
            int uid = int.Parse(Console.ReadLine());
            Console.Write("\tEnter Admin Password: ");
            string pass = ReadPassword();
            var validate = Validate(uid, pass);

            if (validate)
            {
                Console.Clear();
                AdminOption();
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("Invalid Admin-id or Password \n--------Try Again------");
                Console.WriteLine();
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
            Console.Clear();
            Console.WriteLine("===================================================================================");
            Console.WriteLine("\t------Welcome To Admin Portal------");
            Console.WriteLine("===================================================================================");
           
            Console.WriteLine("\t1. Add Train Press '1'");
            Console.WriteLine("\t2. Modify Train Press '2'");
            Console.WriteLine("\t3. Delete Train Press '3'");
            Console.WriteLine("\t4. Show Trains Press '4'");
            Console.WriteLine("\t5. Exit... Press '5'");
            Console.Write("Your Choice: ");
            int inst = int.Parse(Console.ReadLine());
            

            if (inst == 1)
            {
                //Add Train
                Add_Train();
            }
            else if (inst == 2)
            {
                Console.Clear();
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t------Train Modification Portal------");
                Console.WriteLine("===================================================================================");
                UpdateTrainName();
            }
            else if (inst == 3)
            {
                //delete train
                Console.Clear();
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t------Train Deletion and Activation\\Deactivation Portal------");
                Console.WriteLine("===================================================================================");
                Console.WriteLine("\t1. For Permanently Delete Press '1'");
                Console.WriteLine("\t2. For Activate the Train Press '2'");
                Console.WriteLine("\t3. For Deactivate The train Press '3'");
                Console.WriteLine("\t4. For Exit Press '4'");
                Console.Write("your Choice: ");
                start:
                int ans = int.Parse(Console.ReadLine());
                if (ans == 1)
                {
                    Console.WriteLine("----------------------------------------------------------");
                    Console.WriteLine("Sorry!!!");
                    Console.WriteLine("Don't have permission to permanently delete any train.....");
                    Console.WriteLine("Press Tab\\Enter to continue.....");
                    Console.WriteLine("----------------------------------------------------------");
                    Console.ReadKey();
                   
                    Console.Clear();
                    AdminOption();
                }
                    
                else if (ans == 2)
                    ActivateTrain();
                else if (ans == 3)
                {
                    DeActivateTrain();
                }
                   
                else if (ans == 4)
                {
                    Console.Clear();
                    AdminOption();
                }
                else
                {
                    Console.WriteLine("Wrong Choice Try Again.....");
                    goto start;
                }
                    

            }
            else if (inst == 4)
            {
                Console.Clear();
                Show_Train();
                Console.WriteLine();
                Console.Write("Press Tab\\Enter to Continue....");
                Console.ReadKey();
                Console.Clear();
                AdminOption();
            }
            else if (inst == 5)
            {
                Program.ReservationPortal();
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
            Console.Clear();
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
        //static void Delete_Train()
        //{
        //    Show_Train();
        //    Console.Write("Enter Train No you want to delete :");
        //    int trainno = int.Parse(Console.ReadLine());
        //    var TrainToRemove = RRS.Train_Details.SingleOrDefault(t => t.Train_No == trainno);
        //    if (TrainToRemove != null)
        //    {
        //        Console.Write("\nAre you sure you want to delete the Train 'Y/N': ");
        //        string ans = Console.ReadLine().ToUpper();
        //        if (ans == "Y")
        //        {
        //            RRS.Train_Details.Remove(TrainToRemove);
        //            RRS.SaveChanges();
        //            Console.WriteLine("\n---Sucessfully Deleted---");
        //            Show_Train();
        //            AdminOption();
        //        }
        //        else
        //            AdminOption();
        //    }
        //    else
        //    {
        //        Console.WriteLine("\n ---Train can't be found please select a try form the list---");
        //        AdminOption();
        //    }

        //}

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
                {
                    Console.Clear();
                    AdminOption();
                }
                    
            }
            else
            {
                Console.WriteLine("----Selected Train can't be Found----");
                Console.Write("Press Tab\\Enter to Continue....");
                Console.Clear();
                AdminOption();
            }

        }
        //method for deactivate the train
        static void DeActivateTrain()
        {
            Console.Clear();
            Console.WriteLine("=======================================================================");
            Console.WriteLine("\t Train  Deativation Portal.....");
            Console.WriteLine("=======================================================================");
            Show_Train();
            Console.Write("Enter The Train No which you want to Deactivate: ");
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
                {
                    Console.Clear();
                    AdminOption();
                }
                    
            }
            else
            {
                Console.WriteLine("Enter a Valid Train No...");
                Console.WriteLine("Press Tab\\Enter to Continue...");
                Console.ReadKey();
                Console.Clear();
                DeActivateTrain();
            }
            AdminOption();
        }
        static void ActivateTrain()
        {
            Console.Clear();
            Console.WriteLine("=======================================================================");
            Console.WriteLine("\t Train Activation Portal.....");
            Console.WriteLine("=======================================================================");
            Show_Train();
            Console.Write("Enter The Train No which you want to Activate: ");
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
                    Console.WriteLine("Press Tab\\Enter to Continue...");
                    Console.ReadKey();
                    Console.Clear();
                }
                else
                    AdminOption();
            }
            else
            {
                Console.WriteLine("Enter a Valid Train No...");
                ActivateTrain();
            }
            AdminOption();
        }
        static void Show_Train()
        {
            Console.WriteLine("=======================================================================");
            Console.WriteLine("\t All Available Trains.....");
            Console.WriteLine("=======================================================================");
            var trains = RRS.Train_Details.ToList();
            int cnt = 1;
            Console.WriteLine($"->\tTrain-No\t\tTrain-Name\t\tSource\t\tDestination\t\tStatus");
            foreach (var train in trains)
            {
                Console.WriteLine($"{cnt}\t{train.Train_No}\t\t\t{train.Train_Name}\t\t{train.Source}\t{train.Destination}\t\t{train.Train_Status}");
                cnt++;
            }
        }
        public static string ReadPassword()
        {
            string pass = "";
            ConsoleKeyInfo key;

            do
            {
                key = Console.ReadKey(true);

                // Ignore any key that's not a backspace or Enter
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    // Append the character to the password
                    pass += key.KeyChar;
                    Console.Write("*"); // Print '*' instead of the actual character
                }
                else if (key.Key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    // If backspace is pressed, remove the last character from the password
                    pass = pass.Substring(0, (pass.Length - 1));
                    Console.Write("\b \b"); // Erase the character from the console
                }
            }
            while (key.Key != ConsoleKey.Enter);

            return pass;
        }
    }
}
