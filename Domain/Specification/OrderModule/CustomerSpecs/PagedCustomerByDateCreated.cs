using Domain.Entity.Model.Order;
using Domain.Specification.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.CustomerSpecs
{
    public class PagedCustomerByDateCreated:BaseSpecification<Customer>
    {

        public PagedCustomerByDateCreated(PagingParams pagingParams)
        {

            AddSorting(x => x.OrderByDescending(c => c.DateCreated));
            AddPaging(new Paging(pagingParams));

        }
    }
}
