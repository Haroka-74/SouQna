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
        }
    }
}