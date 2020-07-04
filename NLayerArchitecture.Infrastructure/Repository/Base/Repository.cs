using Microsoft.EntityFrameworkCore;
using NLayerArchitecture.Core.Entities;
using NLayerArchitecture.Core.Repositories;
using NLayerArchitecture.Core.Specification.Base;
using NLayerArchitecture.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace NLayerArchitecture.Infrastructure.Repository.Base
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
         protected readonly NLayerArchitectureDbContext _dbContext;

        public Repository(NLayerArchitectureDbContext dbContext)
        {
            this._dbContext = dbContext??throw new ArgumentNullException(nameof(dbContext));
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).ToListAsync();
        }
        public async Task<int> CountAsync(ISpecification<TEntity> specification)
        {
            return await ApplySpecification(specification).CountAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await _dbContext.Set<TEntity>().Where(predicate).ToListAsync();
        }
        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, 
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, 
                                                    string includeString = null,
                                                    bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) query = query.Include(includeString);

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate = null, 
                                                    Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
                                                    List<Expression<Func<TEntity, object>>> includes = null,
                                                    bool disableTracking = true)
        {
            IQueryable<TEntity> query = _dbContext.Set<TEntity>();
            if (disableTracking) query = query.AsNoTracking();

            if (includes != null) query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            return await query.ToListAsync();
        }
        


        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.Now;
            _dbContext.Set<TEntity>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            entity.EditedDate = DateTime.Now;
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
        #region private methods
        private IQueryable<TEntity> ApplySpecification(ISpecification<TEntity> specification)
        {
            return SpecificationEvaluator<TEntity>.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);
        }
        #endregion
    }
}
