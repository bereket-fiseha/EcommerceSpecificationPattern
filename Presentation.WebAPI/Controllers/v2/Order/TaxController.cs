using Application.Interface;
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers.v2.Order
{
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/taxs")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _TaxService;
      public  TaxController(ITaxService TaxService)
        {
            _TaxService = TaxService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxQueryDTO>>> GetAllTaxs() {

             
            return Ok(await _TaxService.GetAllTaxsAsync());
        }
          [HttpGet("{id}", Name = "GetTaxById")]
        public async Task<ActionResult<TaxQueryDTO>> GetTaxById(Guid id)
                                         => Ok(await _TaxService.GetTaxByIdAsync(id));
        

        [HttpPost]
        public async Task<ActionResult<TaxQueryDTO>> CreateTax([FromBody]TaxCommandDTO tax)
        { 
            if (tax == null)
                return BadRequest("Tax entity is null.");
            else if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            tax.Id = Guid.Empty;
           await _TaxService.CreateTaxAsync(tax);
            return CreatedAtRoute(nameof(GetTaxById), new { id = tax.Id }, tax);
        }


    }
}
