using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.CartAggregate;

namespace SouQna.Application.Features.Carts.Commands.AddItemToCart
{
    public class AddItemToCartCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<AddItemToCartCommand>
    {
        public async Task Handle(
            AddItemToCartCommand command,
            CancellationToken cancellationToken
        )
        {
            if(!await unitOfWork.Users.AnyAsync(u => u.Id == command.UserId))
                throw new NotFoundException($"User with ID {command.UserId} not found");

            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == command.ProductId
            ) ?? throw new NotFoundException($"Product with ID {command.ProductId} not found");

            var cart = await unitOfWork.Carts.FindAsync(
                c => c.UserId == command.UserId,
                c => c.CartItems
            );

            if(cart is null)
            {
                cart = Cart.Create(command.UserId);
                await unitOfWork.Carts.AddAsync(cart);
            }

            cart.AddItem(product, command.Quantity);
            await unitOfWork.SaveChangesAsync();
        }
    }
}