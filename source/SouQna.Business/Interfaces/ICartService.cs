using SouQna.Business.Contracts.Requests.Carts;
using SouQna.Business.Contracts.Responses.Carts;

namespace SouQna.Business.Interfaces
{
    public interface ICartService
    {
        Task<CartResponse> GetCartAsync(Guid userId);
        Task AddToCartAsync(Guid userId, AddToCartRequest request);
        Task UpdateCartItemAsync(Guid userId, Guid productId, UpdateCartItemRequest request);
        Task RemoveFromCartAsync(Guid userId, Guid productId);
    }
}