using MediatR;

namespace SouQna.Application.Features.Carts.Customer.GetCart
{
    public record GetCartRequest(
        Guid UserId
    ) : IRequest<DTOs.CartDTO>;
}