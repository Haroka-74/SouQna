using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Categories.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCategoryByIdQuery, GetCategoryByIdResponse>
    {
        public async Task<GetCategoryByIdResponse> Handle(
            GetCategoryByIdQuery query,
            CancellationToken cancellationToken
        )
        {
            var category = await unitOfWork.Categories.FindAsync(
                c => c.Id == query.Id,
                c => c.Subcategories
            ) ?? throw new NotFoundException($"Category with ID {query.Id} not found");

            return new GetCategoryByIdResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Subcategories = [.. category.Subcategories.Select(c => c.Name)]
            };
        }
    }
}