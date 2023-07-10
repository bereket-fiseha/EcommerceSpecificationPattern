using Application.Interface;
using Domain.Common;
using Domain.Entity.DTO.OrderModule.CustomerDTOS;
using Domain.Entity.DTO.OrderModule.ItemDTOS;
using Domain.Entity.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Model.Order;
using Domain.Entity.Parameters;
using Domain.Exceptions;
using Domain.Interface.DomainLogic;
using Domain.Interface.Repository.Common;
using Domain.Specification.OrderModule.OrderDetailSpecs;
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

        public async Task DeleteOrderDetailAsync(Guid id)
        {

            var record = await _orderDetailRepository.GetByIdAsync(id);

            _orderDetailRepository.Delete(record);
     
            //on delete substruct from order cart
            var orderCart = await _orderCartRepository.GetByIdAsync(record.OrderCartId);
            if (orderCart != null)
            {
                orderCart.TotalPrice -= record.NetTotalPrice;
                _orderCartRepository.Update(orderCart);
            }
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsAsync(OrderDetailParams orderDetailParams)
        {
            var spec = new PagedOrderDetailsByDateCreated(orderDetailParams);

            var orderDetails =await _orderDetailRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<OrderDetailQueryDTO>>(orderDetails);
        }
        public async Task<IEnumerable<CustomerOrderDetailQueryDTO>> GetAllCustomerOrderDetailsAsync(OrderDetailParams orderDetailParams)
        {
            var spec = new PagedCustomerOrderDetailsWithItemsByDateCreated(orderDetailParams);
            var customerOrderDetails = await _orderDetailRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<CustomerOrderDetailQueryDTO>>(customerOrderDetails);


        }
        public async Task<IEnumerable<OrderDetailQueryDTO>> GetAllOrderDetailsWithItemByOrderCartId(OrderDetailParams orderDetailParams)
        {
            var spec = new PagedOrderDetailWithItemsFilteredByOrderCartIdByDateCreated(orderDetailParams);                                                                                                                                                                      
             var orderDetails = await _orderDetailRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<OrderDetailQueryDTO>>(orderDetails);
        }

        public Task<OrderDetailQueryDTO> GetOrderDetailByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateOrderDetailAsync(OrderDetailCommandDTO orderDetail)
        {
            var duplicateEntity = await _orderDetailRepository.GetByConditionAsync(filter: (x => x.ItemId == orderDetail.ItemId && x.OrderCartId != orderDetail.OrderCartId));
            if (duplicateEntity.Any())
            {
                throw new DuplicateEntityException(nameof(OrderDetail), nameof(OrderDetail.ItemId), orderDetail.ItemId);
            }
            var oldEntity = await _orderDetailRepository.GetByIdAsync(orderDetail.Id);
            var entity = _mapper.Map<OrderDetail>(orderDetail);
            _orderDetailRepository.Update(entity);
            await _unitOfWork.SaveChangeAsync();
            if (oldEntity.NetTotalPrice != orderDetail.NetTotalPrice) {
                var orderCart = await _orderCartRepository.GetByIdAsync(orderDetail.OrderCartId);
                if (orderCart != null)
                {
                    orderCart.TotalPrice -= oldEntity.NetTotalPrice;
                    orderCart.TotalPrice += orderDetail.NetTotalPrice;

                    _orderCartRepository.Update(orderCart);
                }

            }
            _unitOfWork.SaveChangeAsync();
        }
    }
}
