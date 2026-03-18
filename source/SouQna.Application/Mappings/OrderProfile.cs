using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Features.Orders.Shared;

namespace SouQna.Application.Mappings
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderDTO>();
        }
    }
}