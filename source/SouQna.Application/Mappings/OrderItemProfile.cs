using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Features.Orders.Shared;

namespace SouQna.Application.Mappings
{
    public class OrderItemProfile : Profile
    {
        public OrderItemProfile()
        {
            CreateMap<OrderItem, OrderItemDTO>()
                .ForCtorParam("ItemName", opt => opt.MapFrom(oi => oi.ItemName))
                .ForCtorParam("ItemImage", opt => opt.MapFrom(oi => oi.ItemImage))
                .ForCtorParam("ItemPrice", opt => opt.MapFrom(oi => oi.ItemPrice))
                .ForCtorParam("ItemQuantity", opt => opt.MapFrom(oi => oi.ItemQuantity))
                .ForCtorParam("Subtotal", opt => opt.MapFrom(oi => oi.ItemPrice * oi.ItemQuantity));
        }
    }
}