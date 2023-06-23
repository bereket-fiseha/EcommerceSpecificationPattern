using Application.Interface;
using Domain.Common;
using Domain.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.Order;
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

        public async Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesAsync(int pageNum,int pageSize)
        {

            var paging = new Paging(pageSize: pageSize, pageNum: pageNum);

            var spec = new PagedItemCategoryByDateCreatedSpec(take:paging.Take,skip:paging.Skip);

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

        public async Task<IEnumerable<ItemCatogoryQueryDTO>> GetAllItemCategoriesWithItems(int pageNum,int pageSize)
        {
            var paging = new Paging(pageSize:pageSize,pageNum:pageNum) ;
            var spec = new PagedItemCategoryWithItemsByDateCreatedSpec(skip:paging.Skip,take:paging.Take);
            var itemCategories = await _itemCategoryRepository.GetBySpecificationAsync(spec);

            return _mapper.Map<IEnumerable<ItemCatogoryQueryDTO>>(itemCategories);
        }
      
        public async Task<ItemCatogoryQueryDTO> GetItemCategoryByIdAsync(Guid id)
        {
            return _mapper.Map<ItemCatogoryQueryDTO>(await _itemCategoryRepository.GetByIdAsync(id));
        }

        public async Task CreateItemCategoryAsync(ItemCategoryCommandDTO record)
        {   var itemCategory = _mapper.Map<ItemCategory>(record);
            _itemCategoryRepository.Create(itemCategory);

        await    _unitOfWork.SaveChangeAsync();
            record.Id = itemCategory.Id;

        }

        public Task DeleteItemCategoryAsync(ItemCategoryCommandDTO ItemCategory)
        {
            throw new NotImplementedException();
        }


        public Task UpdateItemCategoryAsync(ItemCategoryCommandDTO ItemCategory)
        {
            throw new NotImplementedException();
        }

    }
}
