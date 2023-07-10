using Domain.Enum.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO.OrderModule.CustomerDTOS
{
    public class CustomerOrderDetailQueryDTO:BaseEntity
    {

        public Guid UserId { get; set; }



        public string CustomerName { get; set; }



        public string Phone { get; set; }



        public string Address { get; set; }
        public string Item { get; set; }
        public string ItemCategory { get; set; }

        public decimal Discount { get; set; }


        public int Quantity { get; set; }


        public decimal TotalTax { get; set; }

        public decimal NetTotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public DateTime OrderDate { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }



    }
}
