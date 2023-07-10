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
    public class ItemCategoryFilteredByIdWithItemsSpec : BaseSpecification<ItemCategory>
    {

        public ItemCategoryFilteredByIdWithItemsSpec(Guid categoryId):base(x=>x.Id==categoryId) {

            AddIncludes(x => x.Include(i => i.Items));
         
        }
    }
}
