using Domain.Entity.Identity;
using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Registration
{
    public class Customer:User
    {
        
        public ICollection<OrderCart> OrderCarts { get; set; }
    }
}
