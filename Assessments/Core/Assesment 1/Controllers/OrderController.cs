using Assesment_1.Models;
using Assesment_1.Models.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Assesment_1.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public IActionResult AllOrders()
        {
            var orders = _orderRepository.GetAll();
            return View(orders);
        }

        [HttpGet]
        public IActionResult PlaceOrder()
        {
            var products = _orderRepository.GetAllProducts();
           
            return View(products);
        }
        public IActionResult PlaceOrder(Order order)
        {
            _orderRepository.PlaceOrder(order);
            return View();
        }
        public IActionResult GetOrderById(int id)
        {
            return (IActionResult)_orderRepository.GetOrderById(id);
        }

        
    }
}
