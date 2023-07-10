using Domain.Entity.Model.Order;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.CustomerSpecs
{
    public class PagedCustomerWithOrderCartsByDateCreated : BaseSpecification<Customer>
    {

        public PagedCustomerWithOrderCartsByDateCreated(PagingParams pagingParams)
        {

            AddSorting(x => x.OrderByDescending(c => c.DateCreated));
            AddPaging(new Paging(pagingParams));
            AddIncludes(x => x.Include(c => c.OrderCarts).ThenInclude(o => o.OrderDetails).ThenInclude(o => o.Item));

        }
    }
}
