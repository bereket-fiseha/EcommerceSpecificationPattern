using Domain.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IItemCategoryService
    {

        public Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesAsync(int pageNum, int pageSize);
        public Task<ItemCatogoryQueryDTO> GetItemCategoryWithItemsById(Guid categoryId);
        public Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesWithItems(int pageNum, int pageSize);

        public Task<ItemCatogoryQueryDTO> GetItemCategoryByIdAsync(Guid id);

        public Task CreateItemCategoryAsync(ItemCategoryCommandDTO record);


        public Task UpdateItemCategoryAsync(ItemCategoryCommandDTO record);


        public Task DeleteItemCategoryAsync(ItemCategoryCommandDTO ItemCategory);







    }
}
