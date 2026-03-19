using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.DTOs.Orders;

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