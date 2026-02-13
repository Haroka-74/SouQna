using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Interfaces
{
    public interface IProductService
    {
        Task<AddProductResponse> AddProductAsync(AddProductRequest request);
    }
}