
using Application.Interface;
using Application.Service;
using Domain.Common;

using Domain.Entity.DTO.OrderModule.OrderDTOS;

using Microsoft.AspNetCore.Mvc;
using Presentation.WebAPI.ActionValidation;
using System.Net;
using Domain.Exceptions;

namespace Presentation.WebAPI.Controllers.v1.Order
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/taxs")]
    public class TaxController : ControllerBase
    {
        private readonly ITaxService _taxService;
      public  TaxController(ITaxService TaxService)
        {
            _taxService = TaxService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TaxQueryDTO>>> GetAllTaxs([FromQuery] PagingParams paging) {


            return Ok(await _taxService.GetAllTaxsAsync(pagingParams: paging));
                
        }
          [HttpGet("{id}", Name = "GetTaxById")]
        public async Task<ActionResult<TaxQueryDTO>> GetTaxById(Guid id)
                                         => Ok(await _taxService.GetTaxByIdAsync(id));
        

        [HttpPost]
        [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<ActionResult<TaxQueryDTO>> CreateTax([FromBody]TaxCommandDTO tax)
        { 
            if (tax == null)
                return BadRequest("Tax entity is null.");
            else if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            tax.Id = Guid.Empty;
           await _taxService.CreateTaxAsync(tax);
            return CreatedAtRoute(nameof(GetTaxById), new { id = tax.Id }, tax);
        }


        [HttpPut]
        //   [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<IActionResult> Updatetax([FromBody] TaxCommandDTO record, Guid id)
        {
            if (record == null || id != record.Id)
            {
                if (record == null)
                    return BadRequest("The tax entity can't be null");
                else
                    return BadRequest("The given id isn't the same as the tax's id");
            }
            else if (!ModelState.IsValid)
                return BadRequest(ModelState);


            try
            {
                var entity = await _taxService.GetTaxByIdAsync(id);
                if (entity == null)
                    return NotFound();


                await _taxService.UpdateTaxAsync(record);
            }
            catch (DuplicateEntityException ex) { return StatusCode((int)HttpStatusCode.Conflict, ex.Message); }

            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }

            return NoContent();

        }

        [HttpDelete]
        //   [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<IActionResult> Deletetax(Guid id)
        {



            if (id == Guid.Empty)
            {
                return BadRequest("The given id can't be empty");
            }

            try
            {
                var entity = await _taxService.GetTaxByIdAsync(id);
                if (entity == null)
                    return NotFound();

                await _taxService.DeleteTaxAsync(id);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error" + ex.Message);
            }



            return NoContent();

        }

    }
}
