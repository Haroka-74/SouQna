using MediatR;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.RemoveFromCart
{
    public record RemoveFromCartRequest(
        Guid UserId,
        Guid ProductId
    ) : IRequest<CartDTO>;
}