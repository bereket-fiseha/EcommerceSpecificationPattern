using Application.Interface;
using Domain.Entity.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Model.Order;
using Domain.Entity.Parameters;
using Domain.Exceptions;
using Domain.Interface.Repository.Common;
using Domain.Specification.OrderModule.OrderCartSpecs;
using Domain.Specification.OrderModule.OrderDetailSpecs;
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
     

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsAsync(OrderCartParams orderCartParams)
        {
            var spec = new PagedOrderCartsByDateCreated(orderCartParams);
            var orderCarts = await _orderCartRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(orderCarts);

        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsByCustomerId(OrderCartParams orderCartParams)
        {
            var spec = new PagedOrderCartsFilteredByCustomerIdByDateCreated(orderCartParams);
            var orderCarts = await _orderCartRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(orderCarts);
        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerId(OrderCartParams orderCartParams)
        {

            var spec = new PagedOrderCartsWithOrderDetailsFilteredByCustomerIdByDateCreated(orderCartParams);
            var orderCarts = await _orderCartRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(orderCarts);
        }

        public async Task<IEnumerable<OrderCartQueryDTO>> GetAllOrderCartsWithOrderDetailsByCustomerIdAndDate(OrderCartParams orderCartParams)
        {
            var spec = new PagedOrderCartsWithOrderDetailsFilteredByCustomerIdByDateCreated(orderCartParams);
            var orderCarts = await _orderCartRepository.GetBySpecificationAsync(spec);
            return _mapper.Map<IEnumerable<OrderCartQueryDTO>>(orderCarts);

        }

        public async Task<OrderCartQueryDTO> GetOrderCartByIdAsync(Guid id)
        {
            return _mapper.Map<OrderCartQueryDTO>(await _orderCartRepository.GetByIdAsync(id));
        }


        public async Task UpdateOrderCartAsync(OrderCartCommandDTO record)
        {
            var duplicateEntity = await _orderCartRepository.GetByConditionAsync(filter: (x => x.OrderDate == record.OrderDate && x.Id != record.Id));
            if (duplicateEntity.Any())
            {
                throw new DuplicateEntityException(nameof(OrderCart), nameof(Item.Name), record.OrderDate);
            }
            var orderCart = _mapper.Map<OrderCart>(record);
            _orderCartRepository.Update(orderCart);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task DeleteOrderCartAsync(Guid id)
        {
            var record = await _orderCartRepository.GetByIdAsync(id);

            _orderCartRepository.Delete(record);
            await _unitOfWork.SaveChangeAsync();
        }
    }
}
