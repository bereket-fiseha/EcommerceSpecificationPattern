using Domain.Interface.Specification;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Repository.Common
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IEnumerable<TEntity>> GetAllAsync<Tkey>(Func<TEntity, Tkey>? orderBy = null, Func<TEntity, Tkey>? orderByDesc = null);
        Task<IEnumerable<TEntity>> GetByConditionAsync(Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>,IOrderedQueryable<TEntity>> orderBy = null,
             Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null);

        Task<IEnumerable<TEntity>> GetBySpecificationAsync(ISpecification<TEntity>? entity=null);




        Task<TEntity?> GetByIdAsync(Guid id);
        Task<TEntity?> FirstOrDefaultAsync();
        Task<TEntity?> LastOrDefaultAsync();
        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);


    }
}
