using Application.Interface;
using AutoMapper;
using Domain.DTO.Order.CustomerDTOS;
using Domain.Entity.Order;
using Domain.Entity.Registration;
using Domain.Interface.Repository.Common;
using Domain.Specification.OrderModule;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Application.Service
{
    public sealed class CustomerService : ICustomerService
    {

        private readonly IGenericRepository<Customer> _CustomerRepository;
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
      public  CustomerService(IGenericRepository<Customer> CustomerRepository,IUnitOfWork unitOfWork,IMapper mapper) {
        _CustomerRepository = CustomerRepository;
            _unitOfWork = unitOfWork;
            _mapper=mapper;
        }

        public async Task<IEnumerable<CustomerQueryDTO>> GetAllCustomersAsync()
        {
           var customers= await _CustomerRepository.GetAllAsync(orderBy:x=>x.Id);
              return _mapper.Map<IEnumerable<CustomerQueryDTO>>(customers);
    
        }
        public async Task<IEnumerable<CustomerQueryDTO>> GetAllCustomersWithOrderCartsAsync()
        {

            //   var Customers = await _CustomerRepository.GetAllCustomersWithOrderCart(orderBy: x => x.Id);
            //var Customers = await _CustomerRepository.GetByConditionAsync(
            //    include:x=>x.Include(c=>c.OrderCarts)
            //    .ThenInclude(o=>o.OrderDetails)
            //    .ThenInclude(o=>o.Item)
            //    .ThenInclude(i=>i.ItemCategory),
            //    orderBy:c=>c.OrderBy(c=>c.Id) );

            var spec = new CustomerWithOrderCartSortedByIdSpecification();
            var customers = await _CustomerRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<CustomerQueryDTO>>(customers);
        }
        public async Task<CustomerQueryDTO> GetCustomerByIdAsync(Guid id)
        {
      
            var customer = await _CustomerRepository.GetByIdAsync(id);
            return _mapper.Map<CustomerQueryDTO>(customer);
        }

        public async Task CreateCustomerAsync([FromBody]CustomerCommandDTO record)
        {
            var customer =_mapper.Map<Customer>(record);
            _CustomerRepository.Create(customer);

        await    _unitOfWork.SaveChangeAsync();
            record.Id = customer.Id;
        }

        public Task DeleteCustomerAsync(CustomerCommandDTO Customer)
        {
            throw new NotImplementedException();
        }


        public Task UpdateCustomerAsync(CustomerCommandDTO Customer)
        {
            throw new NotImplementedException();
        }

       
    }
}
