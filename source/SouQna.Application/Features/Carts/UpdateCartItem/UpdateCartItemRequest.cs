using MediatR;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.UpdateCartItem
{
    public record UpdateCartItemRequest(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest<CartDTO>;
}