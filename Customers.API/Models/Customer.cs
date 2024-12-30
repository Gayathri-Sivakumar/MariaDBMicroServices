namespace Customers.API.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }  // Added phone property
        public string Address { get; set; }  // Added address property
    }
}
