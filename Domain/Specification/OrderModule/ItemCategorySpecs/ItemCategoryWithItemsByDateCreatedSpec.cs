using Domain.Entity.Order;
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
    public class ItemCategoryWithItemsByDateCreatedSpec:BaseSpecification<ItemCategory>
    {

        public ItemCategoryWithItemsByDateCreatedSpec() {

            AddIncludes(x => x.Include(i => i.Items));
            AddSorting(x => x.OrderByDescending(i=>i.DateCreated));
        
        }
    }
}
