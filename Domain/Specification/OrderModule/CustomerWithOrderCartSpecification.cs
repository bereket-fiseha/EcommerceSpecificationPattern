using Domain.Entity.Registration;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule
{
    public class CustomerWithOrderCartSortedByIdSpecification : BaseSpecification<Customer>
    {

       public CustomerWithOrderCartSortedByIdSpecification() {

            AddIncludes(x => x.Include(c => c.OrderCarts)
                        .ThenInclude(c => c.OrderDetails));
                   //      .ThenInclude(o => o.Item));

            AddSorting(x => x.OrderBy(c => c.Id));
        
        
        }
    }
}
