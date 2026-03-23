using MediatR;

namespace SouQna.Application.Features.Carts.Customer.RemoveFromCart
{
    public record RemoveFromCartRequest(
        Guid UserId,
        Guid ProductId
    ) : IRequest<DTOs.CartDTO>;
}