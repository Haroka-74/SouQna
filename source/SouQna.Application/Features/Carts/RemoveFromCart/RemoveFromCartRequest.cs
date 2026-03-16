using MediatR;
using SouQna.Application.Features.Carts.Shared;

namespace SouQna.Application.Features.Carts.RemoveFromCart
{
    public record RemoveFromCartRequest(
        Guid UserId,
        Guid ProductId
    ) : IRequest<CartDTO>;
}