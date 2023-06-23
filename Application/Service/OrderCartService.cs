using Application.Interface;
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using Domain.Entity.Registration;
using Domain.Interface.Repository.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service
{
    public class OrderCartService : IOrderCartService
    {private readonly IGenericRepository<OrderCart> _orderCartRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public OrderCartService(IGenericRepository<OrderCart> orderCartRepository, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _orderCartRepository = orderCartRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateOrderCartAsync(OrderCartCommandDTO record)
        {
            var orderCart = _mapper.Map<OrderCart>(record);
            _orderCartRepository.Create(orderCart);

            await _unitOfWork.SaveChangeAsync();
            record.Id = orderCart.Id;
        }

        public async Task<OrderCartQueryDTO> GetOrderCartWithTaxAndCustomerById(Guid id)
        { var orderCarts =( await _orderCartRepository.GetByConditionAsync(filter: x => x.Id == id, include: x => x.Include(o => o.Customer).
                                                                                                      Include(o => o.Tax)));
         var orderCart = orderCarts.FirstOrDefault();
            
            return _mapper.Map<OrderCartQueryDTO>(orderCart);
      
        }
        public Task DeleteOrderCartAsync(OrderCartCommandDTO OrderCart)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsAsync()
        {
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(await _orderCartRepository.GetAllAsync(orderBy:x=>x.Id));
        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsByCustomerId(Guid customerId)
        {
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(await _orderCartRepository.GetByConditionAsync(filter:x=>x.CustomerId==customerId,orderBy:x=>x.OrderBy(c=>c.Id)));
        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerId(Guid customerId)
        {
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(await _orderCartRepository.GetByConditionAsync(filter:x=>x.CustomerId==customerId,orderBy:x=>x.OrderBy(o=>o.Id),
                       include:x=>x.Include(o=>o.OrderDetails)
                                   .ThenInclude(o=>o.Item)
                                   .ThenInclude(o=>o.ItemCategory)));
        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerIdAndDate(Guid customerId,DateTime? from,DateTime? to)
        {
            var orderCarts = await _orderCartRepository.GetByConditionAsync(filter: x => x.CustomerId == customerId
                                                                                              && x.OrderDate >= from
                                                                                              && x.OrderDate <= to,
                                                                                              orderBy: x => x.OrderBy(o => o.Id),
                                                                                              include: x => x.Include(o => o.OrderDetails)
                                                                                                             .ThenInclude(o => o.Item)
                                                                                                             .ThenInclude(o => o.ItemCategory));


                                  return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(orderCarts);
        }

        public async Task<OrderCartQueryDTO> GetOrderCartByIdAsync(Guid id)
        {
            return _mapper.Map<OrderCartQueryDTO>(await _orderCartRepository.GetByIdAsync(id));
        }


        public Task UpdateOrderCartAsync(OrderCartCommandDTO OrderCart)
        {
            throw new NotImplementedException();
        }
    }
}
