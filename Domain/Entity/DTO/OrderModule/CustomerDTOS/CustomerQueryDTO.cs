using Domain.Entity.Model.IdentityModule;
using Domain.Entity.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.DTO.OrderModule.CustomerDTOS
{
    public class CustomerQueryDTO : User
    {
        public ICollection<OrderCart>? OrderCarts { get; set; } = null;
    }
}
