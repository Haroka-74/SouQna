using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetCartAsync(Guid userId);
        Task UpdateCartItemAsync(Guid userId, Guid productId, UpdateCartItemRequest request);
        Task AddToCartAsync(Guid userId, AddToCartRequest request);
    }
}