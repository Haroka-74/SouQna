using MediatR;
using AutoMapper;
using SouQna.Domain.Entities;
using System.Linq.Expressions;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.Features.Products.Shared;

namespace SouQna.Application.Features.Products.GetProducts
{
    public class GetProductsRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetProductsRequest, Page<ProductDTO>>
    {
        public async Task<Page<ProductDTO>> Handle(
            GetProductsRequest request,
            CancellationToken cancellationToken
        )
        {
            Expression<Func<Product, bool>> filter = p =>
                (string.IsNullOrEmpty(request.SearchTerm) || p.Name.Contains(request.SearchTerm.Trim())) &&
                (!request.MinPrice.HasValue || p.Price >= request.MinPrice) &&
                (!request.MaxPrice.HasValue || p.Price <= request.MaxPrice);

            IOrderedQueryable<Product> orderBy(IQueryable<Product> query)
            {
                return request.SortColumn?.ToLower() switch
                {
                    "price" =>
                        request.SortOrder == "desc"
                        ? query.OrderByDescending(p => p.Price)
                        : query.OrderBy(p => p.Price),
                    "name" =>
                        request.SortOrder == "desc"
                        ? query.OrderByDescending(p => p.Name)
                        : query.OrderBy(p => p.Name),
                    _ =>
                        query.OrderByDescending(p => p.CreatedAt)
                };
            }

            var (items, totalCount) = await unitOfWork.Products.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                filter,
                orderBy
            );

            var products = mapper.Map<List<ProductDTO>>(items);

            return new Page<ProductDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                products.AsReadOnly()
            );
        }
    }
}