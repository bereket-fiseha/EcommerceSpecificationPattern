using Application.Interface;
using Domain.DTO.OrderModule.ItemDTOS;
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using Domain.Entity.Registration;
using Domain.Interface.DomainLogic;
using Domain.Interface.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class OrderDetailService : IOrderDetailService
    {private readonly IGenericRepository<OrderDetail> _orderDetailRepository;
        private readonly IGenericRepository<OrderCart> _orderCartRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IItemRelatedLogic _itemRelatedLogic;
        public OrderDetailService(IGenericRepository<OrderDetail> OrderDetailRepository,IGenericRepository<OrderCart> orderCartRepository,IMapper mapper,IUnitOfWork unitOfWork,IItemRelatedLogic itemRelatedLogic) {
        _orderDetailRepository = OrderDetailRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _itemRelatedLogic = itemRelatedLogic;
            _orderCartRepository = orderCartRepository;
        }

        public async Task CreateOrderDetailAsync(OrderDetailCommandDTO record,ItemQueryDTO item,OrderCartQueryDTO orderCartQuery)
        {
            record.UnitPrice = item.Price;
            Tax? tax = orderCartQuery.Tax;
            record.NetTotalPrice = _itemRelatedLogic.CalculateNetPrice(unitPrice: item.Price, discount: record.Discount, 
                serviceTax: orderCartQuery.Tax?.ServiceTax??0,quantity:record.Quantity);



            var orderDetail = _mapper.Map<OrderDetail>(record);
            _orderDetailRepository.Create(orderDetail);
            var orderCart=await _orderCartRepository.GetByIdAsync(record.OrderCartId);
            if (orderCart != null) {
                orderCart.TotalPrice += orderDetail.NetTotalPrice;
                _orderCartRepository.Update(orderCart);
            }
  
            await _unitOfWork.SaveChangeAsync();
            record.Id = orderDetail.Id;
        }

        public Task DeleteOrderDetailAsync(OrderDetailCommandDTO OrderDetail)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsAsync()
        {
            return _mapper.Map<IEnumerable<OrderDetailQueryDTO>>(await _orderDetailRepository.GetAllAsync(orderBy:x=>x.Id));
        }

        public async Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsWithItemByOrderCartId(Guid orderCartId)
        {
            return _mapper.Map<IEnumerable<OrderDetailQueryDTO>>(await _orderDetailRepository.GetByConditionAsync(filter:o=>o.OrderCartId==orderCartId,
                                                                                                                  include:x=>x.Include(o=>o.Item).ThenInclude(i=>i.ItemCategory),
                                                                                                                   orderBy:x=>x.OrderBy(o=>o.Id)));
        }

        public Task<OrderDetailQueryDTO> GetOrderDetailByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateOrderDetailAsync(OrderDetailCommandDTO OrderDetail)
        {
            throw new NotImplementedException();
        }
    }
}
