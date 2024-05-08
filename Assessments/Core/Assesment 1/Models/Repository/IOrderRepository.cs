namespace Assesment_1.Models.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAll();
        Order GetOrderById(int orderId);
        Order GetOrderDetails(int orderId);
        Order PlaceOrder(Order order);
        List<Product> GetAllProducts();
    }
}
