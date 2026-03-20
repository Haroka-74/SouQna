using MediatR;

namespace SouQna.Application.Features.Orders.ShipOrder
{
    public record ShipOrderRequest(
        Guid Id
    ) : IRequest;
}