using Application.Interface;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;

using Domain.Entity.Model.Order;
using Domain.Exceptions;
using Domain.Interface.Repository.Common;
using Domain.Specification.OrderModule.ItemCategorySpecs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public sealed class ItemCategoryService : IItemCategoryService
    {

        private readonly IGenericRepository<ItemCategory> _itemCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
      public  ItemCategoryService(IGenericRepository<ItemCategory> ItemCategoryRepository,IUnitOfWork unitOfWork, IMapper mapper)
        {
            _itemCategoryRepository = ItemCategoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesAsync(PagingParams pagingParams)
        {


            var spec = new PagedItemCategoryByDateCreatedSpec(pagingParams);

       var itemCategories= await  _itemCategoryRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<ItemCatogoryQueryDTO>>(itemCategories);
        }

        public async Task<ItemCatogoryQueryDTO> GetItemCategoryWithItemsById(Guid categoryId)
        {
            //var itemCategories = await _itemCategoryRepository.GetByConditionAsync(filter: x => x.Id == categoryId, include: x => x.Include(i => i.Items), orderBy: (x) => x.OrderBy(i => i.Id));
            var spec = new ItemCategoryFilteredByIdWithItemsSpec(categoryId:categoryId);
            var itemCategories = await _itemCategoryRepository.GetBySpecificationAsync(spec);

            return _mapper.Map<ItemCatogoryQueryDTO>(itemCategories);
        }

        public async Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesWithItems(PagingParams pagingParams)
        {

            var spec = new PagedItemCategoryWithItemsByDateCreatedSpec(pagingParams);
            var itemCategories = await _itemCategoryRepository.GetBySpecificationAsync(spec);

            return _mapper.Map<IEnumerable<ItemCatogoryQueryDTO>>(itemCategories);
        }
      
        public async Task<ItemCatogoryQueryDTO> GetItemCategoryByIdAsync(Guid id)
        {
            return _mapper.Map<ItemCatogoryQueryDTO>(await _itemCategoryRepository.GetByIdAsync(id));
        }

        public async Task CreateItemCategoryAsync(ItemCategoryCommandDTO record)
        {
            var duplicateEntity =await  _itemCategoryRepository.GetByConditionAsync(x => x.Name == record.Name);
            if (duplicateEntity.Any()) {
                throw new DuplicateEntityException(nameof(ItemCategory), nameof(ItemCategory.Name), record.Name);
            }
            
            var itemCategory = _mapper.Map<ItemCategory>(record);
            _itemCategoryRepository.Create(itemCategory);

        await    _unitOfWork.SaveChangeAsync();
            record.Id = itemCategory.Id;

        }

        public async Task DeleteItemCategoryAsync(Guid id)
        {
            var record = await _itemCategoryRepository.GetByIdAsync(id);

            _itemCategoryRepository.Delete(record);
            await _unitOfWork.SaveChangeAsync();
        }


        public async Task UpdateItemCategoryAsync(ItemCategoryCommandDTO record)
        {
            var duplicateEntity = await _itemCategoryRepository.GetByConditionAsync(filter:(x => x.Name == record.Name&&x.Id!=record.Id));
            if (duplicateEntity.Any())
            {
                throw new DuplicateEntityException(nameof(ItemCategory), nameof(ItemCategory.Name), record.Name);
            }
            var itemCategory = _mapper.Map<ItemCategory>(record);
            _itemCategoryRepository.Update(itemCategory);
            await _unitOfWork.SaveChangeAsync();

        }

    }
}
