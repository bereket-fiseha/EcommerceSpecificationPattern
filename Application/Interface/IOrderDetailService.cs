
using Domain.DTO.OrderModule.ItemDTOS;
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface
{
    public interface IOrderDetailService
    {
        public Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsAsync();

        public  Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsWithItemByOrderCartId(Guid orderCartId);


        public Task<OrderDetailQueryDTO> GetOrderDetailByIdAsync(Guid id);

        public Task CreateOrderDetailAsync(OrderDetailCommandDTO OrderDetail,ItemQueryDTO item, OrderCartQueryDTO orderCartQuery);


        public Task UpdateOrderDetailAsync(OrderDetailCommandDTO OrderDetail);


        public Task DeleteOrderDetailAsync(OrderDetailCommandDTO OrderDetail);

    }
}
