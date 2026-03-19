using MediatR;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.AddToCart
{
    public record AddToCartRequest(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest<CartDTO>;
}