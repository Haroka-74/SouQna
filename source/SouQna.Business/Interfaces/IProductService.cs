using SouQna.Business.Common;
using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductResponse>> GetPagedProductsAsync(GetProductsRequest request);
        Task<ProductResponse> GetProductAsync(Guid id);
        Task<ProductResponse> AddProductAsync(AddProductRequest request);
        Task UpdateProductAsync(Guid id, UpdateProductRequest request);
        Task DeleteProductAsync(Guid id);
    }
}