using Domain.Entity.Model.Order;
using Domain.Entity.Parameters;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.OrderDetailSpecs
{
    public class PagedOrderDetailsByDateCreated:BaseSpecification<OrderDetail>
    {
      public  PagedOrderDetailsByDateCreated(OrderDetailParams orderDetailParams) {
            AddSorting(x => x.OrderByDescending(o => o.DateCreated));
            AddPaging(new Paging(orderDetailParams));
        AddIncludes(x=>x.Include(o=>o.Item));
        }
    }
}
