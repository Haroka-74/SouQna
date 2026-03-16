using MediatR;
using AutoMapper;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.Features.Carts.Shared;

namespace SouQna.Application.Features.Carts.UpdateCartItem
{
    public class UpdateCartItemRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<UpdateCartItemRequest, CartDTO>
    {
        public async Task<CartDTO> Handle(
            UpdateCartItemRequest request,
            CancellationToken cancellationToken
        )
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            ) ?? throw new NotFoundException($"Cart for user (id: {request.UserId}) was not found");

            cart.UpdateItemQuantity(request.ProductId, request.Quantity);
            await unitOfWork.SaveChangesAsync();

            var items = mapper.Map<List<CartItemDTO>>(cart.CartItems);

            return new CartDTO(
                items.Sum(i => i.Quantity),
                items.Sum(i => i.Subtotal),
                items
            );
        }
    }
}