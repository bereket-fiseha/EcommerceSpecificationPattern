using ActionFilters.ActionFilters;
using Application.Interface;
using Domain.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.Order;
using Domain.Specification.OrderModule.ItemCategorySpecs;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.WebAPI.Controllers.v1.Order
{
    [ApiController]
    [Route("/api/v{version:apiVersion}/itemcategories")]
    [ApiVersion("1.0",Deprecated =true)]

    public class ItemCategoryController : ControllerBase
    {
        private readonly IItemCategoryService _ItemCategoryService;
       public ItemCategoryController(IItemCategoryService ItemCategoryService)
        {
            _ItemCategoryService = ItemCategoryService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemCatogoryQueryDTO>>> GetAllItemCategories(int pageNum, int pageSize) {

            
            var ItemCategories=await _ItemCategoryService.GetAllItemCategoriesAsync(pageNum:pageNum,pageSize:pageSize);

            return Ok(ItemCategories);
        }

        [HttpGet("withitems")]
        public async Task<ActionResult<IEnumerable<ItemCatogoryQueryDTO>>> GetAllItemCategoriesWithItems(int pageNum, int pageSize)
        {

            var itemCategories = await _ItemCategoryService.GetAllItemCategoriesWithItems(pageNum: pageNum, pageSize: pageSize);

            return Ok(itemCategories);
        }
        [HttpGet("{id}", Name = "GetItemCategoryById")]
        public async Task<ActionResult<ItemCatogoryQueryDTO>> GetItemCategoryById(Guid id)
        {

            var ItemCategory = await _ItemCategoryService.GetItemCategoryByIdAsync(id);

            return Ok(ItemCategory);
        }

        [HttpPost]
       // [ServiceFilter(typeof(ValidationFilterAttribute))]
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
