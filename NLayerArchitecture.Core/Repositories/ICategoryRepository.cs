using NLayerArchitecture.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Core.Repositories
{
    public interface ICategoryRepository:IRepository<Category>
    {
        Task<Category> GetCategoryWithProductsAsync(int categoryId);

    }
}
