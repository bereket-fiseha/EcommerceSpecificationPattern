
using System.ComponentModel.DataAnnotations;

namespace Domain.Entity.Model.Order
{
    public class Tax : BaseEntity
    {
        [Required(ErrorMessage = "Service Tax is required.")]

        [Range(1, int.MaxValue, ErrorMessage = "Service Tax cannot be 0 or below 0.")]
        public decimal ServiceTax { get; set; }

        //  public decimal chargeTax { get; set; }
        public bool IsActive { get; set; }
        //public  ServiceTaxType  ServiceTaxType{ get;set; }


        public DateTime ActivationStartDate { get; set; } = DateTime.Now;

        public DateTime ActivationEndDate { get; set; }
    }
}
