using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.Specification
{
    public interface ISpecification<TEntity>
    {

        
        Expression<Func<TEntity,bool>> Criteria { get; }

        //List<Expression<Func<TEntity, bool>>> Includes { get; }
        Func<IQueryable<TEntity>, IIncludableQueryable<TEntity,object>> Includes { get; }

        List<string> IncludesString { get; }
        

        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>  Order { get; }

        Expression<Func<TEntity, object>> GroupBy { get; }

        bool IsPagingEnabled { get; }
        int Take { get; }
        
        int Skip { get; }


    }
}
