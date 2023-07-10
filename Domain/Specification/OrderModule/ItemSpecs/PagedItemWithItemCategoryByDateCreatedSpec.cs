using Domain.Entity.Model.Order;
using Domain.Interface.Specification;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.ItemSpecs
{
    public class PagedItemWithItemCategoryByDateCreatedSpec : BaseSpecification<Item>
    {

        public PagedItemWithItemCategoryByDateCreatedSpec(PagingParams pagingParams) {

            AddIncludes(x => x.Include(i => i.ItemCategory));
           AddSorting(x => x.OrderByDescending(i=>i.DateCreated));
            AddPaging(paging: new Paging(pagingParams));
        }
    }
}
