using Application.Interface;
using Application.Service;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Parameters;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers.v1.Order
{
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/v{version:apiVersion}/orderdetails")]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailService _orderDetailService;
        private readonly IOrderCartService _orderCartService;
        private readonly IItemService _itemService;
        public OrderDetailController(IOrderDetailService OrderDetailService, IOrderCartService orderCartService, IItemService itemService)
        {
            _orderDetailService = OrderDetailService;
            _orderCartService = orderCartService;
            _itemService = itemService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderDetailQueryDTO>>> GetAllOrderDetails([FromQuery] OrderDetailParams orderDetailParams)
        {

            var OrderDetails = await _orderDetailService.GetAllOrderDetailsAsync(orderDetailParams);

            return Ok(OrderDetails);
        }

        [HttpGet("customerorderdetails")]
        public async Task<ActionResult<IEnumerable<OrderDetailQueryDTO>>> GetAllCustomerOrderDetails([FromQuery] OrderDetailParams orderDetailParams)
        {

            var OrderDetails = await _orderDetailService.GetAllCustomerOrderDetailsAsync(orderDetailParams);

            return Ok(OrderDetails);
        }
        [HttpGet("ordercartid={orderCartId}")]
        public async Task<ActionResult<IEnumerable<ItemCatogoryQueryDTO>>> GetAllOrderDetailsByOrderCartId([FromQuery] OrderDetailParams orderDetailParams)
        {

            var OrderDetails = await _orderDetailService.GetAllOrderDetailsWithItemByOrderCartId(orderDetailParams);

            return Ok(OrderDetails);
        }



        [HttpGet("{id}", Name = "GetOrderDetailById")]
        public async Task<ActionResult<OrderDetailQueryDTO>> GetOrderDetailById(Guid id)
        {

            var OrderDetail = await _orderDetailService.GetOrderDetailByIdAsync(id);

            return Ok(OrderDetail);
        }

        [HttpPost]
        public async Task<ActionResult<OrderDetailQueryDTO>> CreateOrderDetail([FromBody] OrderDetailCommandDTO record)
        {
            if (record == null)
            {
                return BadRequest("The item entity can't be null");
            }
            if (record.OrderCartId == Guid.Empty)
                ModelState.AddModelError("ordercart_id", "OrderCartId can't be empty");

            else if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var orderCart = await _orderCartService.GetOrderCartWithTaxAndCustomerById(record.OrderCartId);
            if (orderCart == null)
                return NotFound("Order Cart with the specified id is not found.");
            var item = await _itemService.GetItemByIdAsync(record.ItemId);
            if (item == null)
            {
                return NotFound("item with the specified id is not found.");

            }

            record.Id = Guid.Empty;
            await _orderDetailService.CreateOrderDetailAsync(record, item, orderCart);


            return CreatedAtRoute(nameof(GetOrderDetailById), new { id = record.Id }, record);
        }


    }
}
