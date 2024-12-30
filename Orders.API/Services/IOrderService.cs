using Orders.API.Models;

namespace Orders.API.Services
{
    public interface IOrderService
    {
        // Create a new order
        Task<int> InsertAsync(Order order);

        // Get all orders
        Task<List<Order>> FindAllAsync();

        // Get a specific order by ID
        Task<Order> FindOneAsync(int id);

        // Delete an order by ID
        Task<int> DeleteAsync(int id);

        // Update an order
        Task<int> UpdateAsync(Order order);

        // Add an order item to an order
        Task<int> AddOrderItemAsync(OrderItem orderItem);

        // Get all order items for a specific order
        Task<List<OrderItem>> GetOrderItemsAsync(int orderId);
    }
}
