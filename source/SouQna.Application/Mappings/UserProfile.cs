using AutoMapper;
using SouQna.Domain.Entities;
using SouQna.Application.Features.Authentication.RegisterUser;

namespace SouQna.Application.Mappings
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, RegisterUserResponse>();
        }
    }
}