
using Domain.Enum.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO.OrderModule.OrderDTOS
{
    public class OrderCartCommandDTO : BaseEntity
    {

        public DateTime OrderDate { get; set; }

        public decimal TotalPrice { get; set; }



        public OrderStatus OrderStatus { get; set; }

        public Guid TaxId { get; set; }

        public Guid CustomerId { get; set; }

    }
}
