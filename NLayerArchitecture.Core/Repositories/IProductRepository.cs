using NLayerArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Core.Repositories
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductListAsync();
        Task<IEnumerable<Product>> GetProductByNameAsync(string productName);
        Task<IEnumerable<Product>> GetProductByCategoryAsync(int categoryId);
        Task<IEnumerable<Product>> GetProductsWithCategoryAsync();
        Task<Product> GetProductWithCategoryAsync(int productId);
    }
}
