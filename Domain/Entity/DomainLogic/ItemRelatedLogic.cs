using Domain.Interface.DomainLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DomainLogic
{
    public  class ItemRelatedLogic:IItemRelatedLogic
        
    {

        public decimal CalculateNetPrice(decimal unitPrice, decimal discount, decimal serviceTax, int quantity) {

            var totalPrice = unitPrice * quantity;
            var netPrice = totalPrice + totalPrice * (serviceTax/100) - discount;
            return netPrice; 
        
        }
    }
}
