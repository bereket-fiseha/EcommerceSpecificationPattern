using Domain.Common;
using Domain.Entity.Order;
using Domain.Interface.Repository.Common;
using Domain.Interface.Specification;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Common
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ECDbContext _dbContext;
        public GenericRepository(ECDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync<Tkey>(Func<TEntity, Tkey>? orderBy = null, Func<TEntity, Tkey>? orderByDesc = null)
        {

            if (orderBy != null)
                return (await _dbContext.Set<TEntity>().ToListAsync()).OrderBy(orderBy);
            

            else if (orderByDesc != null)
                     return (await _dbContext.Set<TEntity>().ToListAsync()).OrderByDescending(orderByDesc);
            
            return await _dbContext.Set<TEntity>().ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> filter = null, 
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
        {
            IQueryable<TEntity> query  = _dbContext.Set<TEntity>();


            if (include != null) {
                query = include(query);
            }
            if (filter!=null){ 
            query=query.Where(filter);
            }

             if (orderBy != null) {
         
                orderBy(query);
            }

            return await query.ToListAsync();
        }
        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<TEntity>().FindAsync(id);
        }
        public virtual void Create(TEntity entity)
        {
            entity.Id = Guid.Empty;
            entity.DateCreated = DateTime.Now.AddDays(1);
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {

            _dbContext.Set<TEntity>().Remove(entity);
        }


        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }

        public async Task<TEntity?> FirstOrDefaultAsync()
        {
            return await _dbContext.Set<TEntity>().FirstOrDefaultAsync();

        }

        public async Task<TEntity?> LastOrDefaultAsync()
        {
            return await _dbContext.Set<TEntity>().LastOrDefaultAsync();

        }

        public async Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity> specification = null)
        {
         return await   ApplySpec(specification).ToListAsync();
        }

        private IQueryable<TEntity> ApplySpec(ISpecification<TEntity> specification)
        {


            return SpecificationEvaluator<TEntity>.GetQuery(_dbContext.Set<TEntity>().AsQueryable(), specification);

        }

    }
}
