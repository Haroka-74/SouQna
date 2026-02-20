using SouQna.Business.Common;
using SouQna.Business.Contracts.Requests.Products;
using SouQna.Business.Contracts.Responses.Products;

namespace SouQna.Business.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductResponse>> GetProductsAsync(GetProductsRequest request);
        Task<ProductResponse> GetProductAsync(Guid id);
        Task<ProductResponse> AddProductAsync(AddProductRequest request);
        Task UpdateProductAsync(Guid id, UpdateProductRequest request);
        Task DeleteProductAsync(Guid id);
    }
}