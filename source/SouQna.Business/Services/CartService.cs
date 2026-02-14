using SouQna.Business.Interfaces;
using SouQna.Business.Exceptions;
using SouQna.Infrastructure.Entities;
using SouQna.Infrastructure.Interfaces;
using SouQna.Business.Contracts.Requests;
using SouQna.Business.Contracts.Responses;

namespace SouQna.Business.Services
{
    public class CartService(
        IUnitOfWork unitOfWork,
        IValidationService validationService
    ) : ICartService
    {
        public async Task<CartResponse> GetCartAsync(Guid userId)
        {
            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == userId,
                "CartItems.Product"
            );

            if(cart is null || cart.CartItems.Count == 0)
                return new CartResponse(0, 0, []);

            var items = cart.CartItems.Select(item => new CartItemResponse(
                item.Id,
                item.ProductId,
                item.Product.Name,
                item.Product.Image,
                item.PriceAtAddition,
                item.Quantity,
                item.PriceAtAddition * item.Quantity
            )).ToList();

            return new CartResponse(
                items.Sum(i => i.Subtotal),
                items.Sum(i => i.Quantity),
                items
            );
        }

        public async Task UpdateCartItemAsync(Guid userId, Guid productId, UpdateCartItemRequest request)
        {
            await validationService.ValidateAsync(request);

            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == userId,
                c => c.CartItems
            );

            if(cart is null)
                return;

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == productId);

            if(existingItem is null)
                return;

            existingItem.Quantity = request.Quantity;
            await unitOfWork.SaveChangesAsync();
        }

        public async Task AddToCartAsync(Guid userId, AddToCartRequest request)
        {
            await validationService.ValidateAsync(request);

            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.ProductId
            ) ?? throw new NotFoundException($"Product with (id: {request.ProductId}) was not found");

            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == userId,
                c => c.CartItems
            );

            if(cart is null)
            {
                cart = new Cart
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await unitOfWork.Carts.AddAsync(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == request.ProductId);

            if(existingItem is not null)
            {
                existingItem.Quantity += request.Quantity;
            }
            else
            {
                existingItem = new CartItem
                {
                    Id = Guid.NewGuid(),
                    CartId = cart.Id,
                    ProductId = request.ProductId,
                    Quantity = request.Quantity,
                    PriceAtAddition = product.Price
                };

                await unitOfWork.CartItems.AddAsync(existingItem);
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await unitOfWork.SaveChangesAsync();
        }
    }
}