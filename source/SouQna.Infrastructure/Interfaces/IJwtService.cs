using SouQna.Infrastructure.Entities;

namespace SouQna.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string Generate(User user);
    }
}