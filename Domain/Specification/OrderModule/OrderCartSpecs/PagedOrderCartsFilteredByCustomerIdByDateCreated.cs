using Domain.Entity.Model.Order;
using Domain.Entity.Parameters;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.OrderCartSpecs
{
    public class PagedOrderCartsFilteredByCustomerIdByDateCreated : BaseSpecification<OrderCart>
    {
        public PagedOrderCartsFilteredByCustomerIdByDateCreated(OrderCartParams orderCartParams):base(x=>x.CustomerId==orderCartParams.CustomerId) {

            AddSorting(x => x.OrderByDescending(o => o.DateCreated));
            AddPaging(new Paging(orderCartParams));
            AddIncludes(x => x.Include(o => o.Tax));
        }
    }
}
