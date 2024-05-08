namespace Assesment_1.Models.Repository
{
    public interface ICustomerRepository
    {
        List<Customer> GetCustomersByOrderDate(DateTime orderDate);
        Customer GetCustomerWithHighestOrder();
    }
}
