using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entity.Model.Order;

namespace Domain.Entity.DTO.OrderModule.ItemCategoryDTOS
{
    public class ItemCatogoryQueryDTO : BaseEntity
    {
        public ItemCatogoryQueryDTO()
        {
            Items = new HashSet<Item>();
        }

        public required string Name { get; set; }
        public required string Description { get; set; }

        public ICollection<Item> Items { get; set; }
    }
}
