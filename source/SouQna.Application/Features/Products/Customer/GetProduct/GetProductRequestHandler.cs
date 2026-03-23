using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Customer.GetProduct
{
    public class GetProductRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetProductRequest, DTOs.ProductDTO>
    {
        public async Task<DTOs.ProductDTO> Handle(
            GetProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.Id
            ) ?? throw new NotFoundException($"Product with (id: {request.Id}) was not found");

            return new DTOs.ProductDTO(
                product.Id,
                product.Name,
                product.Description,
                product.Price,
                product.Image
            );
        }
    }
}