namespace Products.API.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }  // Added price property
        public int Stock { get; set; }  // Added stock property
    }
}