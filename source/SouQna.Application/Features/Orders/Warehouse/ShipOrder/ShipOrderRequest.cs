using MediatR;

namespace SouQna.Application.Features.Orders.Warehouse.ShipOrder
{
    public record ShipOrderRequest(
        Guid Id
    ) : IRequest;
}