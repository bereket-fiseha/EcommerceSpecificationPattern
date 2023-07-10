using Domain.Entity.Model.Order;
using Domain.Interface.Specification;
using Domain.Specification.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Specification.OrderModule.TaxSpecs
{
    public class PagedTaxByDateCreatedSpec:BaseSpecification<Tax>
    {

        public PagedTaxByDateCreatedSpec(PagingParams pagingParams) {
       

            AddPaging(new Paging(pageNum:pagingParams.PageNumber,pageSize:pagingParams.PageSize));
            AddSorting(x => x.OrderByDescending(i=>i.DateCreated));
        
        }
    }
}
