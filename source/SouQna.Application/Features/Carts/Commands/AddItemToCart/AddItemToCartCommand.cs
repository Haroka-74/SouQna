using MediatR;

namespace SouQna.Application.Features.Carts.Commands.AddItemToCart
{
    public record AddItemToCartCommand(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest;
}