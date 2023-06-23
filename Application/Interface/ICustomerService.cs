
using Domain.DTO.Order.CustomerDTOS;

using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface ICustomerService
    {

        public Task<IEnumerable<CustomerQueryDTO>> GetAllCustomersAsync();

        public Task<IEnumerable<CustomerQueryDTO>> GetAllCustomersWithOrderCartsAsync();



        public Task<CustomerQueryDTO> GetCustomerByIdAsync(Guid id);

        public Task CreateCustomerAsync(CustomerCommandDTO Customer);


        public Task UpdateCustomerAsync(CustomerCommandDTO Customer);


        public Task DeleteCustomerAsync(CustomerCommandDTO Customer);







    }
}
