using NLayerArchitecture.Core.Entities;
using NLayerArchitecture.Core.Specification.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity:BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            string includeString = null,
                                            bool disableTracking = true);
        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null,
                                            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                            List<Expression<Func<TEntity,object>>> includes=null,
                                            bool disableTracking = true);

        Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification);
        Task<TEntity> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<int> CountAsync(ISpecification<TEntity> specification);


    }
}
