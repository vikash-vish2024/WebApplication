using Microsoft.EntityFrameworkCore;

namespace Assesment_1.Models.Repository
{
    public class OrderServices : IOrderRepository
    {
        private readonly NorthwindContext _context;

        public OrderServices(NorthwindContext context)
        {
            _context = context;
        }
        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }
        public Order GetOrderById(int orderId)
        {
            return _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == orderId);
        }

        public Order GetOrderDetails(int orderId)
        {
            return _context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.OrderId == orderId);
        }
        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }
        public Order PlaceOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();
            return order;
        }
    }
}
