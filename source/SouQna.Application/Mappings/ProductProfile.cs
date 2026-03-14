using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Features.Products.Shared;

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