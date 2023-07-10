
using Domain.Common;
using Domain.Entity.DTO.OrderModule.OrderDTOS;

using Domain.Entity.Parameters;
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
        public Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsAsync(OrderCartParams orderCartParms);

        public  Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsByCustomerId(OrderCartParams orderCartParms);

        public  Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerId(OrderCartParams orderCartParms);

        public  Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerIdAndDate(OrderCartParams orderCartParms);

        public Task<OrderCartQueryDTO> GetOrderCartByIdAsync(Guid id);

        public Task CreateOrderCartAsync(OrderCartCommandDTO OrderCart);


        public Task UpdateOrderCartAsync(OrderCartCommandDTO OrderCart);


        public Task DeleteOrderCartAsync(Guid id);

    }
}
