using Application.Interface;
using Application.Service;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemDTOS;
using Domain.Entity.Model.Order;
using Domain.Exceptions;
using Domain.Specification.OrderModule.ItemSpecs;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Web.Http.Results;

namespace Presentation.WebAPI.Controllers.v1.Order
{

    [ApiController]
    [ApiVersion("1.0")]
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
        public async Task<ActionResult<IEnumerable<Item>>> GetAllItems([FromQuery] PagingParams pagingParams) {

            var items=await _itemService.GetAllItemsAsync(pagingParams);

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

            catch (DuplicateEntityException ex) {
                return Conflict(ex.Message);
            }

            catch (Exception ex)
            {

                return StatusCode(500,ex.Message);
            }
            return CreatedAtRoute(nameof(GetItemById), new { id = record.Id },record);
        }

        
        [HttpPut]
     //   [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<IActionResult> UpdateItem([FromBody] ItemCommandDTO record,Guid id)
        {
            if (record == null || id !=record.Id)
            {if (record == null)
                    return BadRequest("The item entity can't be null");
                else
                    return BadRequest("The given id isn't the same as the item's id");
            }
            else if (!ModelState.IsValid)
                return BadRequest(ModelState);

       
            try
            {
                var entity = await _itemService.GetItemByIdAsync(id);
                if (entity == null)
                    return NotFound();
               
                
                await _itemService.UpdateItemAsync(record);
            }
            catch (DuplicateEntityException ex) { return StatusCode((int)HttpStatusCode.Conflict, ex.Message); }

            catch (Exception ex) {
                return StatusCode(500, "Internal Server Error" + ex.Message); 
            }
        
            return NoContent();

        }

        [HttpDelete]
        //   [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<IActionResult> DeleteItem( Guid id)
        {



            if (id==Guid.Empty)
            {
                    return BadRequest("The given id can't be empty");
            }
            
            try
            {
                var entity = await _itemService.GetItemByIdAsync(id);
                if (entity == null)
                    return NotFound();

                await _itemService.DeleteItemAsync(id);
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal Server Error" + ex.Message); 
            }
          
            
            
            return NoContent();

            }


    }
}
