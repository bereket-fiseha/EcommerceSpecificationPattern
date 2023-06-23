
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderCartService
    {
        public Task<OrderCartQueryDTO> GetOrderCartWithTaxAndCustomerById(Guid id);
        public Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsAsync();

        public  Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsByCustomerId(Guid customerId);

        public  Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerId(Guid customerId);

        public  Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerIdAndDate(Guid customerId,DateTime? from,DateTime? to);

        public Task<OrderCartQueryDTO> GetOrderCartByIdAsync(Guid id);

        public Task CreateOrderCartAsync(OrderCartCommandDTO OrderCart);


        public Task UpdateOrderCartAsync(OrderCartCommandDTO OrderCart);


        public Task DeleteOrderCartAsync(OrderCartCommandDTO OrderCart);

    }
}
