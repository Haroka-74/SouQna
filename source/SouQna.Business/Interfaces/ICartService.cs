using SouQna.Business.Contracts.Requests;

namespace SouQna.Business.Interfaces
{
    public interface ICartService
    {
        Task AddToCartAsync(Guid userId, AddToCartRequest request);
    }
}