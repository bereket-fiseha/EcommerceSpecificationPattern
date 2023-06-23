using Domain.Interface.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Common
{
 public    class SpecificationEvaluator<TEntity>
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,ISpecification<TEntity> specification) {
            var query = inputQuery;

            if (specification.Criteria != null) { 
            query= query.Where(specification.Criteria);
            
            }

            if (specification.Includes != null) {
                query = specification.Includes(query);
            
            }
            if (specification.Order != null)
            {

                query=specification.Order(query);

            }
            if (specification.GroupBy != null) {

                query = query.GroupBy(specification.GroupBy).SelectMany(x => x);
            
            }
            if (specification.IsPagingEnabled) { 
         
                query = query.Skip(specification.Skip);
                query = query.Take(specification.Take);
            }

            return query;

        }

    }
}
