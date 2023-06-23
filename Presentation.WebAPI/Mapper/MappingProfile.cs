using AutoMapper;
using Domain.DTO.Order.CustomerDTOS;
using Domain.DTO.OrderModule.ItemCategoryDTOS;
using Domain.DTO.OrderModule.ItemDTOS;
using Domain.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Order;
using Domain.Entity.Registration;

namespace Presentation.WebAPI.Mapper
{
    public class MappingProfile:Profile
    {

        public MappingProfile()
        {




            #region OrderModule

            CreateMap<Item, ItemQueryDTO>();
            CreateMap<ItemCommandDTO, Item>();


            CreateMap<Tax, TaxQueryDTO>();
            CreateMap<TaxCommandDTO, Tax>();


            CreateMap<ItemCategory, ItemCatogoryQueryDTO>();
            CreateMap<ItemCategoryCommandDTO, ItemCategory>();


            CreateMap<Customer, CustomerQueryDTO>();
            CreateMap<CustomerCommandDTO, Customer>();



            CreateMap<OrderCart, OrderCartQueryDTO>();
            CreateMap<OrderCartCommandDTO, OrderCart>();


            CreateMap<OrderDetail, OrderDetailQueryDTO>();
            CreateMap<OrderDetailCommandDTO, OrderDetail>();




            #endregion

        }

    }
}
