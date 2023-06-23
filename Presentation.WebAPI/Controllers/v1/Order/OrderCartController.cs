using Application.Interface;
using Application.Service;
using Domain.DTO.OrderModule.ItemCategoryDTOS;
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers.v1.Order
{
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/ordercarts")]
    public class OrderCartController : ControllerBase
    {
        private readonly IOrderCartService _orderCartService;

        private readonly ICustomerService _customerService;
        public OrderCartController(IOrderCartService orderCartService, ICustomerService customerService)
        {
            _orderCartService = orderCartService;
            _customerService = customerService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderCartQueryDTO>>> GetAllOrderCarts() {

            var OrderCarts=await _orderCartService.GetAllOrderCartsAsync();

            return Ok(OrderCarts);
        }

        [HttpGet("withorderdetails/cutomerId{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderCartQueryDTO>>> GetAllOrderCartsWithOrderDetailsByCustomerId(Guid customerId)
        {

            var orderCarts = await _orderCartService.GetAllOrderCartsWithOrderDetailsByCustomerId(customerId);

            return Ok(orderCarts);
        }
        [HttpGet("cutomerId{customerId}")]
        public async Task<ActionResult<IEnumerable<OrderCartQueryDTO>>> GetAllOrderCartsByCustomerId(Guid customerId)
        {

            var orderCarts = await _orderCartService.GetAllOrderCartsByCustomerId(customerId);

            return Ok(orderCarts);
        }

        [HttpGet("{id}", Name = "GetOrderCartById")]
        public async Task<ActionResult<OrderCartQueryDTO>> GetOrderCartById(Guid id)
        {

            var OrderCart = await _orderCartService.GetOrderCartByIdAsync(id);

            return Ok(OrderCart);
        }

        [HttpPost]
        public async Task<ActionResult<OrderCartQueryDTO>> CreateOrderCart([FromBody]OrderCartCommandDTO record)
        {
            if (record == null)
            {
                return BadRequest("The item entity can't be null");
            }
            if (record.CustomerId == Guid.Empty)
                ModelState.AddModelError("customer_id", "CustomerId can't be empty");

            else if (!ModelState.IsValid)
                    return BadRequest(ModelState);
            var customer = await _customerService.GetCustomerByIdAsync(record.CustomerId);
            if (customer == null)
                return NotFound("Customer with the specified id is not found.");
            record.Id = Guid.Empty;
            await _orderCartService.CreateOrderCartAsync(record);

            return CreatedAtRoute(nameof(GetOrderCartById), new { id = record.Id }, record);
        }


    }
}
