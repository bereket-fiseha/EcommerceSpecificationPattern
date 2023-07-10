
using Application.Interface;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.Model.Order;
using Domain.Specification.OrderModule.ItemCategorySpecs;
using Microsoft.AspNetCore.Mvc;
using Presentation.WebAPI.ActionValidation;

namespace Presentation.WebAPI.Controllers.v2.Order
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/itemcategories")]
    [ApiVersion("2.0")]

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
           


            if (record == null)
            {
                return BadRequest("The item entity can't be null");
            }
            else if (!ModelState.IsValid)
                    return BadRequest(ModelState);
            
            try
            {
                await _ItemCategoryService.CreateItemCategoryAsync(record);
            }
            catch (Exception ex) { return StatusCode(500, "Internal Server Error"+ex.Message); }
            return StatusCode(201);

            // return CreatedAtRoute(nameof(GetItemCategoryById), new { id = record.Id }, record);
        }


    }
}
