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
    public class PagedOrderCartsWithOrderDetailsFilteredByCustomerIdAndDateByDateCreated : BaseSpecification<OrderCart>
    {
        public PagedOrderCartsWithOrderDetailsFilteredByCustomerIdAndDateByDateCreated(OrderCartParams orderCartParams)
            :base(x=>x.CustomerId==orderCartParams.CustomerId&&
                            x.DateCreated>orderCartParams.from&&
                             x.DateCreated<orderCartParams.to) {

            AddSorting(x => x.OrderByDescending(o => o.DateCreated));
            AddPaging(new Paging(orderCartParams));
            AddIncludes(x => x.Include(o => o.Tax).Include(o => o.OrderDetails).ThenInclude(o => o.Item).ThenInclude(i=>i.ItemCategory));
        }
    }
}
