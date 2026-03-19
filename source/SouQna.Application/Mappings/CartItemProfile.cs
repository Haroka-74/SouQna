using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Mappings
{
    public class CartItemProfile : Profile
    {
        public CartItemProfile()
        {
            CreateMap<CartItem, CartItemDTO>()
                .ForCtorParam("Id", opt => opt.MapFrom(ci => ci.Id))
                .ForCtorParam("ProductId", opt => opt.MapFrom(ci => ci.ProductId))
                .ForCtorParam("ProductName", opt => opt.MapFrom(ci => ci.Product.Name))
                .ForCtorParam("ProductImage", opt => opt.MapFrom(ci => ci.Product.Image))
                .ForCtorParam("Quantity", opt => opt.MapFrom(ci => ci.Quantity))
                .ForCtorParam("PriceSnapshot", opt => opt.MapFrom(ci => ci.PriceSnapshot))
                .ForCtorParam("Subtotal", opt => opt.MapFrom(ci => ci.Quantity * ci.PriceSnapshot));
        }
    }
}