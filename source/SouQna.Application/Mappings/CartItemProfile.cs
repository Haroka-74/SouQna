using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Features.Carts.Shared;

namespace SouQna.Application.Mappings
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemDTO>()
                .ForCtorParam("Id", opt => opt.MapFrom(s => s.Id))
                .ForCtorParam("ProductId", opt => opt.MapFrom(s => s.ProductId))
                .ForCtorParam("ProductName", opt => opt.MapFrom(s => s.Product.Name))
                .ForCtorParam("ProductImage", opt => opt.MapFrom(s => s.Product.Image))
                .ForCtorParam("Quantity", opt => opt.MapFrom(s => s.Quantity))
                .ForCtorParam("PriceSnapshot", opt => opt.MapFrom(s => s.PriceSnapshot))
                .ForCtorParam("Subtotal", opt => opt.MapFrom(s => s.Quantity * s.PriceSnapshot));
        }
    }
}