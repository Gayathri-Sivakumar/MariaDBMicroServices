using Orders.API.Models;
using Orders.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Orders.API.Services
{
    public sealed class OrderService : IOrderService
    {
        private readonly MariaDbContext _dbContext;

        public OrderService(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Create a new order
        public async Task<int> InsertAsync(Order order)
        {
            if (order.CustomerId <= 0 ||
                order.OrderDate == default ||
                order.TotalAmount <= 0 ||
                string.IsNullOrWhiteSpace(order.Status))
            {
                throw new ArgumentException("CustomerId, OrderDate, TotalAmount, and Status are required.");
            }

            _dbContext.Orders.Add(order);
            await _dbContext.SaveChangesAsync();
            return order.Id; // Returning the ID of the newly created order
        }

        // Get all orders
        public Task<List<Order>> FindAllAsync() => _dbContext.Orders.ToListAsync();

        // Get a specific order by ID
        public async Task<Order> FindOneAsync(int id)
        {
            var order = await _dbContext.Orders.FirstOrDefaultAsync(x => x.Id == id);
            if (order == null)
            {
                // You can throw an exception here if the order is not found.
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }
            return order;
        }

        // Delete an order by ID
        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                var order = await _dbContext.Orders.FindAsync(id);
                if (order == null) return 0; // Return 0 if order doesn't exist

                _dbContext.Orders.Remove(order);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log or handle the concurrency exception
                // return a status code or custom error
                throw new InvalidOperationException("There was an issue deleting the order.", ex);
            }
        }

        // Update an order
        public async Task<int> UpdateAsync(Order order)
        {
            try
            {
                if (order.CustomerId <= 0 ||
                    order.OrderDate == default ||
                    order.TotalAmount <= 0 ||
                    string.IsNullOrWhiteSpace(order.Status))
                {
                    throw new ArgumentException("CustomerId, OrderDate, TotalAmount, and Status are required.");
                }

                _dbContext.Orders.Update(order);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                // Log or handle the concurrency exception
                throw new InvalidOperationException("There was an issue updating the order.", ex);
            }
        }

        // Add an order item to an order
        public async Task<int> AddOrderItemAsync(OrderItem orderItem)
        {
            if (orderItem.OrderId <= 0 ||
                orderItem.ProductId <= 0 ||
                orderItem.Quantity <= 0 ||
                orderItem.Price <= 0)
            {
                throw new ArgumentException("OrderId, ProductId, Quantity, and Price are required.");
            }

            _dbContext.OrderItems.Add(orderItem);
            return await _dbContext.SaveChangesAsync();
        }

        // Get all order items for a specific order
        public Task<List<OrderItem>> GetOrderItemsAsync(int orderId) =>
            _dbContext.OrderItems.Where(x => x.OrderId == orderId).ToListAsync();
    }
}
