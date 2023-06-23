using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.OrderModule.ItemCategoryDTOS
{
    public class ItemCategoryCommandDTO:BaseEntity
    {
        [Required(ErrorMessage ="The name field is required")]
        [MaxLength(80,ErrorMessage ="The max length of the field is 80")]
        public required string Name { get; set; }
        public required string Description { get; set; }

    }
}
