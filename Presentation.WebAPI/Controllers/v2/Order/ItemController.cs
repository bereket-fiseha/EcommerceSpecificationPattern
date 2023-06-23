using Application.Interface;
using Domain.DTO.OrderModule.ItemDTOS;
using Domain.Entity.Order;
using Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Results;

namespace Presentation.WebAPI.Controllers.v2.Order
{
    [ApiVersion("2.0")]
    [Route("/api/v{version:apiVersion}/items")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IItemCategoryService _itemCategoryService;
        public  ItemController(IItemService itemService, IItemCategoryService itemCategoryService)
        {
            _itemService = itemService;
            _itemCategoryService = itemCategoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems() {

            var items=await _itemService.GetAllItemsAsync();

            return Ok(items);
        }
          [HttpGet("{id}", Name = "GetItemById")]
        public async Task<ActionResult<Item>> GetItemById(Guid id)
        {

            var item = await _itemService.GetItemByIdAsync(id);

            return Ok(item);
        }

        [HttpPost]
        public async Task<ActionResult<Item>> CreateItem([FromBody]ItemCommandDTO record)
        {
            if(record == null)
            {
                return BadRequest("The item entity can't be null");
            }
            if (record.ItemCategoryId == Guid.Empty)
                ModelState.AddModelError("item_category_id", "ItemCategoryId can't be empty");
            
            else if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var itemCategory= await _itemCategoryService.GetItemCategoryByIdAsync(record.ItemCategoryId);
            if (itemCategory == null)
                return NotFound("Item Category with the specified id is not found.");
            record.Id = Guid.Empty;
            try
            {
                await _itemService.CreateItemAsync(record);
            }

            catch (EntityAlreadyExistsException ex) {
                return BadRequest(ex.Message);
                    
            }
            catch(Exception ex)
            {

                return StatusCode(500,ex.Message);
            }
            return CreatedAtRoute(nameof(GetItemById), new { id = record.Id },record);
        }


    }
}
