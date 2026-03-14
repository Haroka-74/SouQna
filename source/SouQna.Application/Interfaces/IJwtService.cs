using SouQna.Domain.Entities;

namespace SouQna.Application.Interfaces
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}