using MediatR;
using SouQna.Domain.Entities;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.CreateProduct
{
    public class CreateProductRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<CreateProductRequest, AdminProductDTO>
    {
        public async Task<AdminProductDTO> Handle(
            CreateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            using var memoryStream = new MemoryStream();
            await request.ImageStream.CopyToAsync(memoryStream, cancellationToken);

            var product = Product.Create(
                request.Name,
                request.Description,
                Convert.ToBase64String(memoryStream.ToArray()),
                request.Price
            );

            var inventory = Inventory.Create(
                product.Id,
                request.Quantity
            );

            await unitOfWork.Products.AddAsync(product);
            await unitOfWork.Inventories.AddAsync(inventory);
            await unitOfWork.SaveChangesAsync();

            return new AdminProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Image,
                product.CreatedAt,
                inventory.Quantity
            );
        }
    }
}