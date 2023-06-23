using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Order;
namespace Domain.DTO.OrderModule.ItemCategoryDTOS
{
    public class ItemCatogoryQueryDTO:BaseEntity
    {
      public  ItemCatogoryQueryDTO()
        {
            Items = new HashSet<Item>();
        }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
