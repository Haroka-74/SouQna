using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Interfaces
{
    public interface IAuthService
    {
        Task<RegisterUserResponse> RegisterAsync(RegisterUserRequest request);
        Task<LoginUserResponse> LoginAsync(LoginUserRequest request);
    }
}