using Domain.Entity.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO.OrderModule.ItemDTOS
{
    public class ItemQueryDTO : BaseEntity
    {
        public required string Name { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }
        public Guid ItemCategoryId { get; set; }

        public virtual ItemCategory ItemCategory { get; set; }
    }
}
