using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReservationSys.Business_Layers.User;
using ReservationSys.Business_Layers.Admin;
namespace ReservationSys
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ReservationPortal();
            }
            catch(Exception e)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine($"\tError Occur: {e.GetType()} " +
                    $"\n\tMassage: {e.Message}");
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("---------------------------");
                Console.WriteLine("Sorry For Inconvenience...");
                Console.WriteLine("---------------------------\n");

                Console.WriteLine("Press Tab\\Enter to Continue....");
                Console.ReadKey();
                Console.Clear();
                ReservationPortal();
            }
        }

        public static void ReservationPortal()
        {
            Console.Clear();
        start:
            Console.WriteLine("====================================================================");
            Console.WriteLine("\t------Welcome to Railway Resevation System------");
            Console.WriteLine("====================================================================");
            Console.WriteLine(@"
                           _________________                  
                  ________|     |______|_____\________        
                 |   |_|_|_|_|_|_|_|_|_|_|_|_|_|_|  o |
                -|__|-----------------------------|___|
                  |_|[][_]                    |_|[][_]
                  
                        ");

            Console.WriteLine("Press Enter/Tab to Continue....");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine("\t------Welcome to Railway Resevation System------");
            Console.WriteLine("====================================================================");
            Console.WriteLine("\n\t1. Admin Login Press '1'\n\t2. User Login Press '2'\n\t3. Log of From Railway Portal Press '3'");
            Console.Write("your Choice :");
            int inst = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.Clear();
            

            if (inst == 1)
            {
                AdminFunction.Admin_Login();
            }
            else if (inst == 2)
            {
                Console.WriteLine("=======================================================================");
                Console.WriteLine("\t---Welcome User To Railway Reservation System---");
                Console.WriteLine("=======================================================================");
                UserFunction.User_Login();
            }
            else if (inst == 3)
            {
                Console.WriteLine("\n\n\t\t<<<<<Thank you for Your Time.....>>>>>");
                System.Threading.Thread.Sleep(3000);
                Environment.Exit(0);
            }
                
            else
            {
                Console.WriteLine();
                Console.WriteLine("----------------------------------");
                Console.WriteLine("Please Choose a valid Option......");
                Console.WriteLine("----------------------------------");
                Console.WriteLine();
                Console.WriteLine("Press Tab\\Enter to Continue....");
                Console.ReadKey();
                Console.Clear();
                goto start;
            }

            Console.Read();
        }
    
    }
}
