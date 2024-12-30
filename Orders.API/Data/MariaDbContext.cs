using Orders.API.Models;
using Microsoft.EntityFrameworkCore;
using Products.API.Models;

namespace Orders.API.Data
{
    public partial class MariaDbContext : DbContext
    {
        public MariaDbContext(DbContextOptions<MariaDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Order>(b =>
            {
                b.ToTable("Orders");
                b.HasKey(x => x.Id);
                b.Property(x => x.TotalAmount).HasColumnType("decimal(18,2)");
                b.Property(x => x.OrderDate).HasColumnType("datetime");
                b.Property(x => x.Status).HasMaxLength(50);

                // Ensure that the CustomerId is properly mapped to the 'customer_id' column in the database
                b.Property(x => x.CustomerId).HasColumnName("customer_id");
            });

            builder.Entity<OrderItem>(b =>
            {
                b.ToTable("OrderItems");
                b.HasKey(x => new { x.OrderId, x.ProductId }); // Composite key
                b.Property(x => x.Quantity).HasDefaultValue(1);
                b.Property(x => x.Price).HasColumnType("decimal(18,2)");
            });

            // Relationship between Order and OrderItems
            builder.Entity<Order>()
                .HasMany(o => o.OrderItems)
                .WithOne()
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
