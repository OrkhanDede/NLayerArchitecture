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
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        public CategoryRepository(NLayerArchitectureDbContext dbContext)
            :base(dbContext)
        {

        }
        public async Task<Category> GetCategoryWithProductsAsync(int categoryId)
        {
            //refactor need
            var category =await _dbContext.Categories.
                                    Where(x => x.Id == categoryId).
                                    Include(x => x.Products).
                                    FirstOrDefaultAsync();
            return category;
        }
    }
}
