
using Application.Interface;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.Model.Order;
using Domain.Exceptions;
using Domain.Specification.OrderModule.ItemCategorySpecs;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebAPI;
using Presentation.WebAPI.ActionValidation;
using System.Net;

namespace Presentation.WebAPI.Controllers.v1.Order
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/itemcategories")]
    [ApiVersion("1.0")]

    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _ItemCategoryService;
       public ItemCategoryController(IItemCategoryService ItemCategoryService)
        {
            _ItemCategoryService = ItemCategoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCatogoryQueryDTO>>> GetAllItemCategories([FromQuery]PagingParams pagingParams) {

            
            var ItemCategories=await _ItemCategoryService.GetAllItemCategoriesAsync(pagingParams);

            return Ok(ItemCategories);
        }

        [HttpGet("withitems")]
        public async Task<ActionResult<IEnumerable<ItemCatogoryQueryDTO>>> GetAllItemCategoriesWithItems([FromQuery] PagingParams pagingParams)
        {

            var itemCategories = await _ItemCategoryService.GetAllItemCategoriesWithItems(pagingParams);

            return Ok(itemCategories);
        }
        [HttpGet("{id}", Name = "GetItemCategoryById")]
        public async Task<ActionResult<ItemCatogoryQueryDTO>> GetItemCategoryById(Guid id)
        {

            var ItemCategory = await _ItemCategoryService.GetItemCategoryByIdAsync(id);

            return Ok(ItemCategory);
        }

        [HttpPost]
      [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<ActionResult<ItemCategory>> CreateItemCategory([FromBody]ItemCategoryCommandDTO record)
        {
           


            //if (record == null)
            //{
            //    return BadRequest("The item entity can't be null");
            //}
            //else if (!ModelState.IsValid)
            //        return BadRequest(ModelState);
            
            try
            {
                await _ItemCategoryService.CreateItemCategoryAsync(record);
            }

            catch (DuplicateEntityException ex) { return StatusCode((int)HttpStatusCode.Conflict,ex.Message ); }

            catch (Exception ex) { return StatusCode(500, "Internal Server Error"+ex.Message); }
            return StatusCode(201);

            // return CreatedAtRoute(nameof(GetItemCategoryById), new { id = record.Id }, record);
        }

        [HttpPut]
     //   [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<IActionResult> UpdateItemCategory([FromBody] ItemCategoryCommandDTO record,Guid id)
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
                var entity = await _ItemCategoryService.GetItemCategoryByIdAsync(id);
                if (entity == null)
                    return NotFound();
               
                
                await _ItemCategoryService.UpdateItemCategoryAsync(record);
            }
            catch (DuplicateEntityException ex) { return StatusCode((int)HttpStatusCode.Conflict, ex.Message); }

            catch (Exception ex) {
                return StatusCode(500, "Internal Server Error" + ex.Message); 
            }
        
            return NoContent();

        }

        [HttpDelete]
        //   [ServiceFilter(typeof(ModelStateValidation))]
        public async Task<IActionResult> DeleteItemCategory( Guid id)
        {



            if (id==Guid.Empty)
            {
                    return BadRequest("The given id can't be empty");
            }
            
            try
            {
                var entity = await _ItemCategoryService.GetItemCategoryByIdAsync(id);
                if (entity == null)
                    return NotFound();

                await _ItemCategoryService.DeleteItemCategoryAsync(id);
            }
            catch (Exception ex) {
                return StatusCode(500, "Internal Server Error" + ex.Message); 
            }
          
            
            
            return NoContent();

            }



    }
}
