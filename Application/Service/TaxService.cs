using Application.Interface;
using AutoMapper;
using Domain.DTO.OrderModule.OrderDTOS;

using Domain.Entity.Order;
using Domain.Interface.Repository.Common;


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

        public async Task<IEnumerable<TaxQueryDTO>> GetAllTaxsAsync()
        {
           var Taxs= await _taxRepository.GetAllAsync(orderBy:x=>x.Id);
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

        public Task DeleteTaxAsync(TaxCommandDTO Tax)
        {
            throw new NotImplementedException();
        }


        public Task UpdateTaxAsync(TaxCommandDTO Tax)
        {
            throw new NotImplementedException();
        }
    }
}
