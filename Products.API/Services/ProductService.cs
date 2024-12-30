using Microsoft.EntityFrameworkCore;
using Products.API.Data;
using Products.API.Models;

namespace Products.API.Services
{
    public sealed class ProductService : IProductService
    {
        private readonly MariaDbContext _dbContext;

        public ProductService(MariaDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> DeleteAsync(int id)
        {
            try
            {
                _dbContext.Products.Remove(new Product { Id = id });
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }

        public Task<List<Product>> FindAllAsync() => _dbContext.Products.ToListAsync();

        public Task<Product> FindOneAsync(int id) => _dbContext.Products.FirstOrDefaultAsync(x => x.Id == id);

        public async Task<int> InsertAsync(Product product)
        {
            if (string.IsNullOrWhiteSpace(product.Name) ||
                string.IsNullOrWhiteSpace(product.Description) ||
                product.Price <= 0 ||
                product.Stock < 0)
            {
                throw new ArgumentException("Invalid product data. Ensure Name, Description are provided, Price is positive, and Stock is non-negative.");
            }

            _dbContext.Add(product);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(Product product)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(product.Name) ||
                    string.IsNullOrWhiteSpace(product.Description) ||
                    product.Price <= 0 ||
                    product.Stock < 0)
                {
                    throw new ArgumentException("Invalid product data. Ensure Name, Description are provided, Price is positive, and Stock is non-negative.");
                }

                _dbContext.Update(product);
                return await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return 0;
            }
        }
    }
}
