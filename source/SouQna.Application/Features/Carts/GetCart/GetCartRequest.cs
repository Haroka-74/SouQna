using MediatR;
using SouQna.Application.DTOs.Carts;

namespace SouQna.Application.Features.Carts.GetCart
{
    public record GetCartRequest(
        Guid UserId
    ) : IRequest<CartDTO>;
}