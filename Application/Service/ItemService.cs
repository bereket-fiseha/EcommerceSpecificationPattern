using Application.Interface;
using AutoMapper;
using Domain.DTO.OrderModule.ItemDTOS;
using Domain.Entity.Order;
using Domain.Exceptions;
using Domain.Interface.Repository.Common;

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application.Service
{
    public sealed class ItemService : IItemService
    {

        private readonly IGenericRepository<Item> _itemRepository;
        private readonly IItemCategoryService _itemCategoryService;

        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
      public  ItemService(IGenericRepository<Item> itemRepository,IUnitOfWork unitOfWork,IMapper mapper) {
        _itemRepository = itemRepository;
            _itemCategoryService = _itemCategoryService;
            _unitOfWork = unitOfWork;
            _mapper=mapper;
        }

        public async Task<IEnumerable<ItemQueryDTO>> GetAllItemsAsync()
        {
           var items= await _itemRepository.GetByConditionAsync(include:x=>x.Include(i=>i.ItemCategory),orderBy:x=>x.OrderBy(i=>i.Id));
              return _mapper.Map<IEnumerable<ItemQueryDTO>>(items);
    
        }

        public async Task<ItemQueryDTO> GetItemByIdAsync(Guid id)
        {
      
            var item = await _itemRepository.GetByConditionAsync(filter:x=>x.Id==id, include: x => x.Include(i => i.ItemCategory));
            return _mapper.Map<ItemQueryDTO>(item);
        }

        public async Task CreateItemAsync([FromBody]ItemCommandDTO record)
        {
            var existingItem= await _itemRepository.GetByConditionAsync(filter:x=>x.Name==record.Name);
            if (existingItem != null) {
                throw new  EntityAlreadyExistsException("Item","name",record.Name);
            }

            var item =_mapper.Map<Item>(record);
            _itemRepository.Create(item);

        await    _unitOfWork.SaveChangeAsync();
            record.Id= item.Id;

        }

        public Task DeleteItemAsync(ItemCommandDTO item)
        {
            throw new NotImplementedException();
        }


        public Task UpdateItemAsync(ItemCommandDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
