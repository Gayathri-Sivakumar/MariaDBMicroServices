using System.ComponentModel.DataAnnotations;
using Customers.API.Models;

namespace Orders.API.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }  // Ensure this matches the database column

        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string Status { get; set; }  // Pending, Completed, etc.

        // Navigation property for OrderItems
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        // Navigation properties (if needed)
        public Customer Customer { get; set; } // If you're using a navigation property for customer
    }


    public class OrderItem
    {
        public int Id { get; set; } // Primary key for the OrderItem

        public int OrderId { get; set; } // Foreign Key to Order

        public int ProductId { get; set; } // Foreign Key to Product

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int Quantity { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal Price { get; set; }
    }
}
