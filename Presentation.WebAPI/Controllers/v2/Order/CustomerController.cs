using Application.Interface;
using Domain.DTO.Order.CustomerDTOS;
using Domain.Entity.Order;
using Domain.Entity.Registration;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers.v2.Order
{
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/customers")]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
      public  CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerQueryDTO>>> GetAllCustomers() {

            var customers=await _customerService.GetAllCustomersAsync();

            return Ok(customers);
        }
        [HttpGet("withordercarts")]
        public async Task<ActionResult<IEnumerable<CustomerQueryDTO>>> GetAllCustomersWithOrderCarts()
        {

            var customers = await _customerService.GetAllCustomersWithOrderCartsAsync();

            return Ok(customers);
        }
        [HttpGet("{id}", Name = "GetCustomerById")]
        public async Task<ActionResult<Customer>> GetCustomerById(Guid id)
        {

            var customer = await _customerService.GetCustomerByIdAsync(id);

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> CreateCustomer([FromBody] CustomerCommandDTO record)
        {
            if (record == null)
            {
                return BadRequest("The item entity can't be null");
            }

            else if (!ModelState.IsValid)
                return BadRequest(ModelState);
            record.Id = Guid.Empty;
            await _customerService.CreateCustomerAsync(record);

            return CreatedAtRoute(nameof(GetCustomerById), new { id = record.Id }, record);
        }


    }
}
