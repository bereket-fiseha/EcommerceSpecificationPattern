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
    public class PagedCustomerOrderDetailsWithItemsByDateCreated : BaseSpecification<OrderDetail>
    {

        public PagedCustomerOrderDetailsWithItemsByDateCreated(OrderDetailParams orderDetailParams)
        {

            AddSorting(x => x.OrderByDescending(c => c.DateCreated));
            AddPaging(new Paging(orderDetailParams));
            AddIncludes(x => x.Include(o => o.Item).ThenInclude(i => i.ItemCategory).Include(c => c.OrderCart).ThenInclude(o=>o.Tax).Include(c=>c.OrderCart).ThenInclude(o=>o.Customer)); 

        }
    }
}
