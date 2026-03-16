using MediatR;
using AutoMapper;
using SouQna.Application.Interfaces;
using SouQna.Application.Features.Carts.Shared;

namespace SouQna.Application.Features.Carts.GetCart
{
    public class GetCartRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetCartRequest, CartDTO>
    {
        public async Task<CartDTO> Handle(
            GetCartRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            );

            if(cart is null)
                return new CartDTO(0, 0m, []);

            var items = mapper.Map<List<CartItemDTO>>(cart.CartItems);

            return new CartDTO(
                items.Sum(i => i.Quantity),
                items.Sum(i => i.Subtotal),
                items
            );
        }
    }
}