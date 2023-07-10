using Application.Interface;
using AutoMapper;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Model.Order;
using Domain.Exceptions;
using Domain.Interface.Repository.Common;
using Domain.Specification.OrderModule.TaxSpecs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application.Service
{
    public sealed class TaxService : ITaxService
    {

        private readonly IGenericRepository<Tax> _taxRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
      public  TaxService(IGenericRepository<Tax> taxRepository,IUnitOfWork unitOfWork,IMapper mapper) {
        _taxRepository = taxRepository;
            _unitOfWork = unitOfWork;
            _mapper=mapper;
        }

        public async Task<IEnumerable<TaxQueryDTO>> GetAllTaxsAsync(PagingParams pagingParams)
        {
            var spec = new PagedTaxByDateCreatedSpec(pagingParams);
           var Taxs= await _taxRepository.GetBySpecificationAsync(spec);
              return _mapper.Map<IEnumerable<TaxQueryDTO>>(Taxs);
    
        }

        public async Task<TaxQueryDTO> GetTaxByIdAsync(Guid id)
        {
      
            var Tax = await _taxRepository.GetByIdAsync(id);
            return _mapper.Map<TaxQueryDTO>(Tax);
        }

        public async Task CreateTaxAsync([FromBody]TaxCommandDTO record)
        {
            var Tax =_mapper.Map<Tax>(record);
            _taxRepository.Create(Tax);
        await    _unitOfWork.SaveChangeAsync();
            record.Id = Tax.Id;
        }



        public async Task UpdateTaxAsync(TaxCommandDTO taxDto)
        {
            var duplicateEntity = await _taxRepository.GetByConditionAsync(filter: (x => x.ServiceTax!=taxDto.ServiceTax));
            if (duplicateEntity.Any())
            {
                throw new DuplicateEntityException(nameof(Tax), nameof(Tax.ServiceTax), taxDto.ServiceTax);
            }
            var tax = _mapper.Map<Tax>(taxDto);
            _taxRepository.Update(tax);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteTaxAsync(Guid id)
        {
            var record = await _taxRepository.GetByIdAsync(id);

            _taxRepository.Delete(record);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
