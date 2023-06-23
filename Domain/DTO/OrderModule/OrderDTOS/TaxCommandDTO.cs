using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO.OrderModule.OrderDTOS
{
    public class TaxCommandDTO:BaseEntity
    {
        [Required(ErrorMessage = "Service Tax is required.")]

        [Range(1, 50, ErrorMessage = "Service Tax cannot be 0 or below 0.")]
        public decimal ServiceTax { get; set; }

        //  public decimal chargeTax { get; set; }
        public bool IsActive { get; set; }
        //public  ServiceTaxType  ServiceTaxType{ get;set; }


        public DateTime ActivationStartDate { get; set; } = DateTime.Now;

        public DateTime ActivationEndDate { get; set; }
    }
}
