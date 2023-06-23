using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.OrderModule.OrderDTOS
{
   public class TaxQueryDTO:BaseEntity
    {

        public decimal ServiceTax { get; set; }

        //  public decimal chargeTax { get; set; }
        public bool IsActive { get; set; }
        //public  ServiceTaxType  ServiceTaxType{ get;set; }


        public DateTime ActivationStartDate { get; set; } = DateTime.Now;

        public DateTime ActivationEndDate { get; set; }
    }
}
