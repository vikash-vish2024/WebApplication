
using Microsoft.EntityFrameworkCore;

namespace Assesment_1.Models.Repository
{
    public class CustomerServices : ICustomerRepository
    {
        private readonly NorthwindContext _context;

        public CustomerServices(NorthwindContext context)
        {
            _context = context;
        }

        public List<Customer> GetCustomersByOrderDate(DateTime orderDate)
        {
            return _context.Customers.Where(c => c.Orders.Any(o => o.OrderDate == orderDate)).ToList();
        }

        public Customer GetCustomerWithHighestOrder()
        {
            return _context.Orders
                .GroupBy(o => o.CustomerId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.First().Customer)
                .FirstOrDefault();
        }
    }
}
