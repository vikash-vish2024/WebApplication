using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Assesment1.Models;
namespace Assesment1.Controllers
{
    public class CodeController : Controller
    {
        // GET: Code
        static NorthWindDataContext db = new NorthWindDataContext();
       
        public ActionResult Index()
        {
            return View();
        }

        //1. First action method should return all customers residing in Germany
        public ActionResult GermanCustomers()
        {
            var germanCustomers = db.Customers.Where(c => c.Country == "Germany").ToList();
            return View(germanCustomers);
        }

        //2. Second action method should return customer details with an orderId==10248
        public ActionResult CustomerWithOrderID()
        {
            var CustWithID = db.Customers.Where(c => c.Orders.Any(o => o.OrderID == 10248)).FirstOrDefault();
            if (CustWithID == null)
            {
                return HttpNotFound();
            }
            return View(CustWithID);
        }

    }
}