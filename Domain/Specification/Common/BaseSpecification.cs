using Domain.Interface.Specification;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.Common
{
    public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    {

        protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
        {
            Criteria = criteria;
        }

        protected BaseSpecification()
        {
        }
        public Expression<Func<TEntity, bool>> Criteria { get; }

        public Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> Includes { get; private set; }

        public List<string> IncludesString { get; private set; }

        public Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> Order { get; private set; }

        public Expression<Func<TEntity, object>> GroupBy { get; private set; }

        public int Take { get; set; }

        public int Skip { get; set; }
        public bool IsPagingEnabled { get; set; }

        protected void AddIncludes(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> includes)
        {

            Includes = includes;
        }

        protected void AddSorting(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> order)
        {

            Order = order;
        }

        protected void AddPaging(int take, int skip)
        {
            IsPagingEnabled = true;
            Take = take;
            Skip = skip;
        }

        protected void AddGroupBy(Expression<Func<TEntity, object>> groupBy)
        {

            GroupBy = groupBy;

        }



    }
}
