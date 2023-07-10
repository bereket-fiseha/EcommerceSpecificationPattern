using Domain.Entity.Model.Order;
using Domain.Interface.Specification;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.ItemCategorySpecs
{
    public class PagedItemCategoryWithItemsByDateCreatedSpec:BaseSpecification<ItemCategory>
    {

        public PagedItemCategoryWithItemsByDateCreatedSpec(PagingParams pagingParams) {
              AddIncludes(x => x.Include(i => i.Items));
            AddSorting(x => x.OrderByDescending(i=>i.DateCreated));
            AddPaging(new Paging(pageNum: pagingParams.PageNumber, pageSize: pagingParams.PageSize));

        }
    }
}
