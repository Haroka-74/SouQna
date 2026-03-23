using MediatR;

namespace SouQna.Application.Features.Carts.Customer.AddToCart
{
    public record AddToCartRequest(
        Guid UserId,
        Guid ProductId,
        int Quantity
    ) : IRequest<DTOs.CartDTO>;
}