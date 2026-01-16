using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Categories;

namespace SouQna.Application.Features.Categories.Queries.GetCategory
{
    public class GetCategoryQueryHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetCategoryQuery, CategoryDTO>
    {
        public async Task<CategoryDTO> Handle(
            GetCategoryQuery query,
            CancellationToken cancellationToken
        )
        {
            var category = await unitOfWork.Categories.FindAsync(
                c => c.Id == query.Id,
                c => c.Subcategories
            ) ?? throw new NotFoundException($"Category with ID {query.Id} not found");

            return new CategoryDTO
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                Subcategories = [.. category.Subcategories.Select(c => c.Name)]
            };
        }
    }
}