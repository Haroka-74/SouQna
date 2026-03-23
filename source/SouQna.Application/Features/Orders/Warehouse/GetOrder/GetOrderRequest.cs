using MediatR;

namespace SouQna.Application.Features.Orders.Warehouse.GetOrder
{
    public record GetOrderRequest(
        Guid OrderId
    ) : IRequest<DTOs.OrderDTO>;
}