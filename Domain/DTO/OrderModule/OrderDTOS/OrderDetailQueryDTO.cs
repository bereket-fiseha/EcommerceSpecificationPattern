using Domain.Entity.Identity;
using Domain.Entity.Order;
using Domain.Enum.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.OrderModule.OrderDTOS
{
    public class OrderDetailQueryDTO:BaseEntity
    {

        public Guid ItemId { get; set; }

        public Guid OrderCartId { get; set; }


        public decimal NetTotalPrice { get; set; }


        public int Quantity { get; set; }


        public decimal UnitPrice { get; set; }

        public decimal Discount { get; set; }


        public virtual Item Item { get; set; }

        public virtual OrderCart OrderCart { get; set; }
    }
}
