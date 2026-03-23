using MediatR;
using SouQna.Domain.Entities;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;

namespace SouQna.Application.Features.Carts.Customer.AddToCart
{
    public class AddToCartRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<AddToCartRequest, DTOs.CartDTO>
    {
        public async Task<DTOs.CartDTO> Handle(
            AddToCartRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.ProductId
            ) ?? throw new NotFoundException($"Product with (id: {request.ProductId}) was not found");

            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == request.UserId,
                "CartItems.Product"
            );

            if(cart is null)
            {
                cart = Cart.Create(request.UserId);
                await unitOfWork.Carts.AddAsync(cart);
            }

            cart.AddItem(product, request.Quantity);
            await unitOfWork.SaveChangesAsync();

            var items = cart.CartItems.Select(
                i => new DTOs.CartItemDTO(
                    i.Id,
                    i.ProductId,
                    i.Product.Name,
                    i.Product.Image,
                    i.Quantity,
                    i.PriceSnapshot,
                    i.Quantity * i.PriceSnapshot
                )
            ).ToList();

            return new DTOs.CartDTO(
                items.Sum(i => i.Quantity),
                items.Sum(i => i.Subtotal),
                items.AsReadOnly()
            );
        }
    }
}