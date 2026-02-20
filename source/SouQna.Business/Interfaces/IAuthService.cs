using SouQna.Business.Contracts.Requests.Authentication;
using SouQna.Business.Contracts.Responses.Authentication;

namespace SouQna.Business.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterResponse> RegisterAsync(RegisterRequest request);
        Task<LoginResponse> LoginAsync(LoginRequest request);
    }
}