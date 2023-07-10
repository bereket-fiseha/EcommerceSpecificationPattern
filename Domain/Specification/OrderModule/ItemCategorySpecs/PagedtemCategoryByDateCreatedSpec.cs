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
    public class PagedItemCategoryByDateCreatedSpec:BaseSpecification<ItemCategory>
    {

        public PagedItemCategoryByDateCreatedSpec(PagingParams pagingParams) {
             AddSorting(x => x.OrderByDescending(i=>i.DateCreated));
            AddPaging(paging: new Paging(pageNum: pagingParams.PageNumber, pageSize: pagingParams.PageSize));

        }
    }
}
