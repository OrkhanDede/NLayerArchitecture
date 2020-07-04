using NLayerArchitecture.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Application.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryModel>> GetCategoryListAsync();
        Task<CategoryModel> GetById(int id);
        Task<CategoryModel> GetCategoryWithProducts(int categoryId);
        Task<CategoryModel> CreateAsync(CategoryModel categoryModel);
    }
}
