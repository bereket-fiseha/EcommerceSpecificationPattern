
using Domain.DTO.OrderModule.ItemDTOS;
using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IItemService
    {

        public Task<IEnumerable<ItemQueryDTO>> GetAllItemsAsync();



        public Task<ItemQueryDTO> GetItemByIdAsync(Guid id);

        public Task CreateItemAsync(ItemCommandDTO item);


        public Task UpdateItemAsync(ItemCommandDTO item);


        public Task DeleteItemAsync(ItemCommandDTO item);







    }
}
