using SouQna.Business.Common;
using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Interfaces
{
    public interface IProductService
    {
        Task<PagedResult<ProductResponse>> GetPagedProductsAsync(GetProductsRequest request);
        Task<ProductResponse> AddProductAsync(AddProductRequest request);
    }
}