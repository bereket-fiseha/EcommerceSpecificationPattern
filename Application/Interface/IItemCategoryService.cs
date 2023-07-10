using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IItemCategoryService
    {

        public Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesAsync(PagingParams pagingParams);
        public Task<ItemCatogoryQueryDTO> GetItemCategoryWithItemsById(Guid categoryId);
        public Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesWithItems(PagingParams pagingParams);

        public Task<ItemCatogoryQueryDTO> GetItemCategoryByIdAsync(Guid id);

        public Task CreateItemCategoryAsync(ItemCategoryCommandDTO record);


        public Task UpdateItemCategoryAsync(ItemCategoryCommandDTO record);


        public Task DeleteItemCategoryAsync(Guid id);







    }
}
