using MediatR;

namespace SouQna.Application.Features.Carts.Commands.UpdateCartItemQuantity
{
    public record UpdateCartItemQuantityCommand(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest;
}