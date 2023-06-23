using Domain.Enum.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Order
{
    public class CustomerOrderDTO
    {

        public Guid UserId { get; set; }



        public string CustomerName { get; set; }



        public string CustomerPhone { get; set; }



        public string Address { get; set; }
        public string ItemName { get; set; }


        public decimal discount { get; set; }


        public int Quantity { get; set; }


        public decimal TotalTax { get; set; }

        public decimal NetTotalPrice { get; set; }

        public OrderStatus OrderStatus { get; set; }

        public PaymentStatus PaymentStatus { get; set; }

        public PaymentMethod PaymentMethod { get; set; }



    }
}
