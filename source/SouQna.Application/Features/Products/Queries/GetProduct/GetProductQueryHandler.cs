using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.Queries.GetProduct
{
    public class GetProductQueryHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetProductQuery, ProductDTO>
    {
        public async Task<ProductDTO> Handle(
            GetProductQuery query,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == query.Id,
                p => p.Category
            ) ?? throw new NotFoundException($"Product with ID {query.Id} not found");

            return new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Image = product.Image,
                Price = product.Price,
                Quantity = product.Quantity,
                IsActive = product.IsActive,
                CategoryName = product.Category.Name
            };
        }
    }
}