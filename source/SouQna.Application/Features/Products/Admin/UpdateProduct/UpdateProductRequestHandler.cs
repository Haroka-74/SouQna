using MediatR;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;

namespace SouQna.Application.Features.Products.Admin.UpdateProduct
{
    public class UpdateProductRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateProductRequest, DTOs.ProductDTO>
    {
        public async Task<DTOs.ProductDTO> Handle(
            UpdateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.Id,
                p => p.Inventory
            ) ?? throw new NotFoundException($"Product with (id: {request.Id}) was not found");

            product.UpdateMetadata(request.Name, request.Description, request.Price);

            if(request.ImageStream is not null)
            {
                using var memoryStream = new MemoryStream();
                await request.ImageStream.CopyToAsync(memoryStream, cancellationToken);
                product.UpdateImage(Convert.ToBase64String(memoryStream.ToArray()));
            }

            await unitOfWork.SaveChangesAsync();

            return new DTOs.ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Image,
                product.Inventory.Quantity,
                product.CreatedAt
            );
        }
    }
}