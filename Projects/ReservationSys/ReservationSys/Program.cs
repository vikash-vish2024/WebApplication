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
            Console.WriteLine("====================================================================");
            Console.WriteLine("\t------Welcome to Railway Resevation System------");
            Console.WriteLine("====================================================================");
            Console.WriteLine(@"
                           _________________                  
                  ________|     |______|_____\________        
                 |   |_|_|_|_|_|_|_|_|_|_|_|_|_|_|  o |___
                -|__|-----------------------------|___|
                  |_|[][_]                    |_|[][_]
                   |__|                        |__|
                        ");
        start:
            Console.WriteLine("Press Enter/Tab to Continue....");
            Console.ReadKey();
            Console.Clear();
            Console.WriteLine("====================================================================");
            Console.WriteLine("\t------Welcome to Railway Resevation System------");
            Console.WriteLine("====================================================================");
            Console.WriteLine("\n\t1. Admin Login Press '1'\n\t2. User Login Press '2':");
            Console.Write("your Choice :");
            int inst = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();
            Console.WriteLine("\n====================================================================");

            if (inst == 1)
            {
               
                AdminFunction.Admin_Login();
            }
            else if (inst == 2)
            {
                Console.WriteLine("\t---Welcome User To Railway Reservation System---");
                UserFunction.User_Login();
            }
            else
            {
                Console.WriteLine("Please Choose a valid Option......");
                goto start;
            }
            
            Console.Read();
        }
    }
}
