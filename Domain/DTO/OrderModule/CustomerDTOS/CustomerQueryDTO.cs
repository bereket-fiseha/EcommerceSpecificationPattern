using Domain.Entity.Identity;
using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.Order.CustomerDTOS
{
    public class CustomerQueryDTO : User
    { 
        public ICollection<OrderCart>? OrderCarts { get; set; } = null;
    }
}
