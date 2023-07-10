
using Domain.Enum.Order;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO.OrderModule.OrderDTOS
{
    public class OrderDetailCommandDTO : BaseEntity
    {


        public Guid ItemId { get; set; }

        public Guid OrderCartId { get; set; }


        public decimal NetTotalPrice { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Quantity can't be 0 or below 0!.")]
        public int Quantity { get; set; }


        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }



    }
}
