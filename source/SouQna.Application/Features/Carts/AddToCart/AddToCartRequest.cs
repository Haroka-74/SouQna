using MediatR;
using SouQna.Application.Features.Carts.Shared;

namespace SouQna.Application.Features.Carts.AddToCart
{
    public record AddToCartRequest(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest<CartDTO>;
}