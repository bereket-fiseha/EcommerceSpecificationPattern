
using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemDTOS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IItemService
    {

        public Task<IEnumerable<ItemQueryDTO>> GetAllItemsAsync(PagingParams pagingParams);



        public Task<ItemQueryDTO> GetItemByIdAsync(Guid id);

        public Task CreateItemAsync(ItemCommandDTO item);


        public Task UpdateItemAsync(ItemCommandDTO item);


        public Task DeleteItemAsync(Guid id);







    }
}
