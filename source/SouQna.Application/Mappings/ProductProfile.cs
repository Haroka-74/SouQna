using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Mappings
{
    public class ProductProfile : Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDTO>();
            CreateMap<Product, AdminProductDTO>()
                .ForCtorParam("Id", opt => opt.MapFrom(p => p.Id))
                .ForCtorParam("Name", opt => opt.MapFrom(p => p.Name))
                .ForCtorParam("Description", opt => opt.MapFrom(p => p.Description))
                .ForCtorParam("Price", opt => opt.MapFrom(p => p.Price))
                .ForCtorParam("Image", opt => opt.MapFrom(p => p.Image))
                .ForCtorParam("CreatedAt", opt => opt.MapFrom(p => p.CreatedAt))
                .ForCtorParam("Quantity", opt => opt.MapFrom(p => p.Inventory != null ? p.Inventory.Quantity : 0));
        }
    }
}