using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Core.Entities;
using NLayerArchitecture.Core.Repositories;
using NLayerArchitecture.Infrastructure.Data;
using NLayerArchitecture.Infrastructure.Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Infrastructure.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        public ProductRepository(NLayerArchitectureDbContext dbContext)
            :base(dbContext)
        {

        }
        public async Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId)
        {
            return await _dbContext.Products
              .Where(x => x.CategoryId == categoryId)
              .ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string productName)
        {
            return await GetAsync(x => x.Name.ToLower().Contains(productName.ToLower()));
        }

        public async Task<IEnumerable<Product>> GetProductListAsync()
        {
            return await GetAllAsync();
        }

        public async Task<IEnumerable<Product>> GetProductsWithCategoryAsync()
        {
            return await _dbContext.Products.Include(x => x.Category).ToListAsync();
        }

        public async Task<Product> GetProductWithCategoryAsync(int productId)
        {
            return await _dbContext.Products.
                            Where(x => x.Id == productId).
                            Include(x => x.Category).
                            FirstOrDefaultAsync();
        }
    }
}
