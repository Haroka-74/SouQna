using MediatR;

namespace SouQna.Application.Features.Carts.Commands.RemoveItemFromCart
{
    public record RemoveItemFromCartCommand(
        Guid UserId,
        Guid ProductId
    ) : IRequest;
}