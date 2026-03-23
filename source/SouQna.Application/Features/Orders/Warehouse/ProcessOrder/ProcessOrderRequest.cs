using MediatR;

namespace SouQna.Application.Features.Orders.Warehouse.ProcessOrder
{
    public record ProcessOrderRequest(
        Guid Id
    ) : IRequest;
}