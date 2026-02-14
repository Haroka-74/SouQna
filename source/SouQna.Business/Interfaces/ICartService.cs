using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetCartAsync(Guid userId);
        Task AddToCartAsync(Guid userId, AddToCartRequest request);
    }
}