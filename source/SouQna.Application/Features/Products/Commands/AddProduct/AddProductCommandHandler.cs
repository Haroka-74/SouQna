using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Domain.Aggregates.ProductAggregate;

namespace SouQna.Application.Features.Products.Commands.AddProduct
{
    public class AddProductCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService
    ) : IRequestHandler<AddProductCommand, AddProductResponse>
    {
        public async Task<AddProductResponse> Handle(
            AddProductCommand command,
            CancellationToken cancellationToken
        )
        {
            if(await unitOfWork.Products.AnyAsync(p => p.Name == command.Name))
                throw new ConflictException($"A product with name '{command.Name}' already exists");

            if(!await unitOfWork.Categories.AnyAsync(c => c.Id == command.CategoryId))
                throw new NotFoundException($"Category with ID {command.CategoryId} not found");

            var imagePath = await fileService.SaveFileAsync(
                command.ImageStream,
                command.ImageFileName,
                "Products"
            );

            var product = Product.Create(
                command.CategoryId,
                command.Name,
                command.Description,
                imagePath,
                command.Price,
                command.Quantity,
                command.IsActive
            );

            await unitOfWork.Products.AddAsync(product);
            await unitOfWork.SaveChangesAsync();

            return new AddProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Quantity = product.Quantity,
                IsActive = product.IsActive,
                CreatedAt = product.CreatedAt
            };
        }
    }
}