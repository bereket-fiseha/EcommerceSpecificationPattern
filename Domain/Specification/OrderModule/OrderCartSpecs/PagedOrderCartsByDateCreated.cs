using Domain.Entity.Model.Order;
using Domain.Entity.Parameters;
using Domain.Specification.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.OrderCartSpecs
{
    public class PagedOrderCartsByDateCreated:BaseSpecification<OrderCart>
    {
     public   PagedOrderCartsByDateCreated(OrderCartParams orderCartParams) {

            AddSorting(x => x.OrderByDescending(o => o.DateCreated));
            AddPaging(new Paging(orderCartParams));
        
        
        }
    }
}
