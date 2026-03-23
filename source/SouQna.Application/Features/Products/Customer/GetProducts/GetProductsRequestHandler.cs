using MediatR;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Customer.GetProducts
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
                (!request.MinPrice.HasValue || p.Price >= request.MinPrice) &&
                (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice);

            Func<IQueryable<Product>, IOrderedQueryable<Product>> orderBy = q =>
                request.SortBy?.ToLower() switch
                {
                    "price" => request.IsDescending ? q.OrderByDescending(p => p.Price) : q.OrderBy(p => p.Price),
                    _ => q.OrderByDescending(p => p.CreatedAt)
                };

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                orderBy
            );

            var products = items.Select(
                i => new DTOs.ProductDTO(
                    i.Id,
                    i.Name,
                    i.Description,
                    i.Price,
                    i.Image
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