using NLayerArchitecture.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductModel>> GetProductListAsync();
        Task<ProductModel> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductModel>> GetProductByNameAsync(string productName);
        Task<IEnumerable<ProductModel>> GetProductByCategoryAsync(int categoryId);

        Task<IEnumerable<ProductModel>> GetProductsWithCategoryAsync();
        Task<ProductModel> GetProductWithCategoryAsync(int productId);
        Task<ProductModel> CreateAsync(ProductModel productModel);
        Task UpdateAsync(ProductModel productModel);
        Task DeleteAsync(ProductModel productModel);
    }
}
