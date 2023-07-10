
using Domain.Common;
using Domain.Entity.DTO.OrderModule.CustomerDTOS;
using Domain.Entity.DTO.OrderModule.ItemDTOS;
using Domain.Entity.DTO.OrderModule.OrderDTOS;

using Domain.Entity.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderDetailService
    {
        public Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsAsync(OrderDetailParams pagingParams);

        public  Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsWithItemByOrderCartId(OrderDetailParams orderDetailParams);
        public Task<IEnumerable<CustomerOrderDetailQueryDTO>> GetAllCustomerOrderDetailsAsync(OrderDetailParams pagingParams);


        public Task<OrderDetailQueryDTO> GetOrderDetailByIdAsync(Guid id);

        public Task CreateOrderDetailAsync(OrderDetailCommandDTO OrderDetail,ItemQueryDTO item, OrderCartQueryDTO orderCartQuery);


        public Task UpdateOrderDetailAsync(OrderDetailCommandDTO OrderDetail);


        public Task DeleteOrderDetailAsync(Guid id);

    }
}
