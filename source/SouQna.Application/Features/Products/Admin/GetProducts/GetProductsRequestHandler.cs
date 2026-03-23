using MediatR;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Admin.GetProducts
{
    public class GetProductsRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetProductsRequest, Page<DTOs.ProductDTO>>
    {
        public async Task<Page<DTOs.ProductDTO>> Handle(
            GetProductsRequest request,
            CancellationToken cancellationToken
        )
        {
            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(request.SearchTerm) || p.Name.Contains(request.SearchTerm.Trim())) &&
                (!request.MaxStockThreshold.HasValue || p.Inventory.Quantity <= request.MaxStockThreshold);

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                q => q.OrderByDescending(p => p.CreatedAt),
                p => p.Inventory
            );

            var products = items.Select(
                i => new DTOs.ProductDTO(
                    i.Id,
                    i.Name,
                    i.Description,
                    i.Price,
                    i.Image,
                    i.Inventory.Quantity,
                    i.CreatedAt
                )
            ).ToList();

            return new Page<DTOs.ProductDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                products.AsReadOnly()
            );
        }
    }
}