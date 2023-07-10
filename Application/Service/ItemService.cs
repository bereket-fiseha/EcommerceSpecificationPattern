using Application.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.DTO.OrderModule.ItemDTOS;
using Domain.Entity.Model.Order;
using Domain.Exceptions;
using Domain.Interface.Repository.Common;
using Domain.Specification.OrderModule.ItemSpecs;
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

        public async Task<IEnumerable<ItemQueryDTO>> GetAllItemsAsync(PagingParams pagingParams)
        {
            var spec = new PagedItemWithItemCategoryByDateCreatedSpec(pagingParams);

            var items = await _itemRepository.GetBySpecificationAsync(spec);
              return _mapper.Map<IEnumerable<ItemQueryDTO>>(items);
    
        }

        public async Task<ItemQueryDTO> GetItemByIdAsync(Guid id)
        {
      
            var item = await _itemRepository.GetByConditionAsync(filter:x=>x.Id==id, include: x => x.Include(i => i.ItemCategory));
            return _mapper.Map<ItemQueryDTO>(item);
        }

        public async Task CreateItemAsync([FromBody]ItemCommandDTO record)
        {
            var duplicateEntity= await _itemRepository.GetByConditionAsync(filter:x=>x.Name==record.Name);
            if (duplicateEntity.Any()) {
                throw new  DuplicateEntityException(nameof(Item),nameof(Item.Name),record.Name);
            }

            var item =_mapper.Map<Item>(record);
            _itemRepository.Create(item);

        await    _unitOfWork.SaveChangeAsync();
            record.Id= item.Id;

        }

        public async Task DeleteItemAsync(Guid id)
        {
            var record = await _itemRepository.GetByIdAsync(id);

            _itemRepository.Delete(record);
            await _unitOfWork.SaveChangeAsync();
        }


        public async Task UpdateItemAsync(ItemCommandDTO record)
        {
            var duplicateEntity = await _itemRepository.GetByConditionAsync(filter: (x => x.Name == record.Name && x.Id != record.Id));
            if (duplicateEntity.Any())
            {
                throw new DuplicateEntityException(nameof(Item), nameof(Item.Name), record.Name);
            }
            var item = _mapper.Map<Item>(record);
            _itemRepository.Update(item);
            await _unitOfWork.SaveChangeAsync();

        }

    }
}
