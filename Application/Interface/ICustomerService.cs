
using Domain.Common;
using Domain.Entity.DTO.OrderModule.CustomerDTOS;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICustomerService
    {

        public Task<IEnumerable<CustomerQueryDTO>> GetAllCustomersAsync(PagingParams pagingParams);

        public Task<IEnumerable<CustomerQueryDTO>> GetAllCustomersWithOrderCartsAsync(PagingParams pagingParams);

       // public Task<IEnumerable<CustomerOrderDetailQueryDTO>> GetAllCustomerOrderDetailsAsync(PagingParams pagingParams);


        public Task<CustomerQueryDTO> GetCustomerByIdAsync(Guid id);

        public Task CreateCustomerAsync(CustomerCommandDTO Customer);


        public Task UpdateCustomerAsync(CustomerCommandDTO Customer);


        public Task DeleteCustomerAsync(CustomerCommandDTO Customer);







    }
}
