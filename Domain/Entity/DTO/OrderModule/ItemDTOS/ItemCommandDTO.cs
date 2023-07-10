
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO.OrderModule.ItemDTOS
{
    public class ItemCommandDTO : BaseEntity
    {
        [Required(ErrorMessage = "The name field is required")]
        [Range(0, int.MaxValue, ErrorMessage = "The price of an item can't be 0 or below 0!")]

        public decimal Price { get; set; }
        [Required(ErrorMessage = "The name field is required")]
        [MaxLength(80, ErrorMessage = "The max length of the field is 80")]
        public required string Name { get; set; }

        public string? Description { get; set; }

        public Guid ItemCategoryId { get; set; }

    }
}
