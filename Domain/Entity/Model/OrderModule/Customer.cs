using Domain.Entity.Model.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entity.Model.Order
{
    public class Customer : User
    {

        public ICollection<OrderCart> OrderCarts { get; set; }

        public string Address { get; set; }
    }
}
