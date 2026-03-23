using MediatR;
using SouQna.Application.Common;

namespace SouQna.Application.Features.Orders.Warehouse.GetConfirmedOrders
{
    public record GetConfirmedOrdersRequest(
        int PageNumber = 1,
        int PageSize = 10
    ) : IRequest<Page<DTOs.OrderDTO>>;
}