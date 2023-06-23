using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interface.DomainLogic
{
    public interface IItemRelatedLogic
    {

        public decimal CalculateNetPrice(decimal unitPrice, decimal discount, decimal serviceTax, int quantity);
    }
}
