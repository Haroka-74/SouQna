using MediatR;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Orders.Warehouse.GetProcessingOrders
{
    public record GetProcessingOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Page<DTOs.OrderDTO>>;
}