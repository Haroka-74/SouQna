using SouQna.Business.Common;
using SouQna.Business.Contracts.Requests.Orders;
using SouQna.Business.Contracts.Responses.Orders;

namespace SouQna.Business.Interfaces
{
    public interface IOrderService
    {
        Task<PagedResult<OrderSummaryResponse>> GetUserOrdersAsync(Guid userId, GetOrdersRequest request);
        Task<OrderDetailResponse> GetOrderAsync(Guid userId, Guid orderId);
        Task<CreateOrderResponse> CreateOrderAsync(Guid userId, CreateOrderRequest request);
    }
}