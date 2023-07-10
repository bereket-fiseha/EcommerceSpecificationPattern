using AutoMapper;
using Domain.Entity.DTO.OrderModule.CustomerDTOS;
using Domain.Entity.DTO.OrderModule.ItemCategoryDTOS;
using Domain.Entity.DTO.OrderModule.ItemDTOS;
using Domain.Entity.DTO.OrderModule.OrderDTOS;
using Domain.Entity.Model.Order;

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
            //CreateMap<Customer, CustomerOrderDetailQueryDTO>().ForMember(x => x.CustomerName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"))
            //                                                  .ForMember(x => x.Phone, opt => opt.MapFrom(s => $"{s.PhoneNumber}"))
            //                                                  .ForMember(x => x.Item, opt => opt.MapFrom(s => $"{s.OrderCarts.FirstOrDefault(x=>x.Id==)}"))
            //                                                  .ForMember(x => x.FullNameWithCardNumber, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}-{s.CardNumber}"))
            //                                                  .ForMember(x => x.FullNameWithCardNumber, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}-{s.CardNumber}"))
            //                                                  .ForMember(x => x.FullNameWithCardNumber, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}-{s.CardNumber}"))
            //                                                   .ForMember(x => x.FullNameWithCardNumber, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}-{s.CardNumber}"))
 
            //    .ForMember(x => x.FullNameWithCardNumber, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}-{s.CardNumber}"))
            //                                 .ForMember(x => x.FullName, opt => opt.MapFrom(s => $"{s.FirstName} {s.LastName}"));
            //;



            CreateMap<OrderCart, OrderCartQueryDTO>();
            CreateMap<OrderCartCommandDTO, OrderCart>();


            CreateMap<OrderDetail, OrderDetailQueryDTO>();
            CreateMap<OrderDetailCommandDTO, OrderDetail>();
            CreateMap<OrderDetail, CustomerOrderDetailQueryDTO>().ForMember(x => x.CustomerName, opt => opt.MapFrom(s => $"{s.OrderCart.Customer.FirstName} {s.OrderCart.Customer.FirstName}"))
                                                              .ForMember(x => x.Phone, opt => opt.MapFrom(s => $"{s.OrderCart.Customer.PhoneNumber}"))
                                                              .ForMember(x => x.Item, opt => opt.MapFrom(s => $"{s.Item.Name}"))
                                                              .ForMember(x => x.ItemCategory, opt => opt.MapFrom(s => $"{s.Item.ItemCategory.Name}"))
                                                              .ForMember(x => x.Discount, opt => opt.MapFrom(s => $"{s.Discount}"))
                                                              .ForMember(x => x.TotalTax, opt => opt.MapFrom(s => $"{s.OrderCart.Tax.ServiceTax}"))
                                                              .ForMember(x => x.OrderStatus, opt => opt.MapFrom(s => $"{s.OrderCart.OrderStatus}"))
                                                              .ForMember(x => x.OrderDate, opt => opt.MapFrom(s => $"{s.OrderCart.OrderDate}"));
                                                            






            #endregion

        }

    }
}
