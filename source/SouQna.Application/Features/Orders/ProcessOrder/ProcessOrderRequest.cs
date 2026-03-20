using MediatR;

namespace SouQna.Application.Features.Orders.ProcessOrder
{
    public record ProcessOrderRequest(
        Guid Id
    ) : IRequest;
}