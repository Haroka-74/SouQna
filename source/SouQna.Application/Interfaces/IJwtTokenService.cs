using SouQna.Domain.Aggregates.UserAggregate;

namespace SouQna.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string Generate(User user);
    }
}