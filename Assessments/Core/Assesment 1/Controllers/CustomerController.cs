using Assesment_1.Models;
using Assesment_1.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Assesment_1.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public IActionResult GetCustomerByOrderDate(DateTime orderDate)
        {
            var customers = _customerRepository.GetCustomersByOrderDate(orderDate);
            return View(customers);
        }
        public IActionResult GetCustomerWithHighestOrder()
        {
            var highestOrderCustomer = _customerRepository.GetCustomerWithHighestOrder();
            return View(highestOrderCustomer);
        }
    }
}
