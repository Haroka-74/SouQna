using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateProductCommand, UpdateProductResponse>
    {
        public async Task<UpdateProductResponse> Handle(
            UpdateProductCommand command,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == command.Id
            ) ?? throw new NotFoundException($"Product with ID {command.Id} not found");

            product.UpdateDetails(command.Name, command.Description, command.Price);
            await unitOfWork.SaveChangesAsync();

            return new UpdateProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price
            };
        }
    }
}