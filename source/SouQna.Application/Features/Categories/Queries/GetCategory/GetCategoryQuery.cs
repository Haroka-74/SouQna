using MediatR;
using SouQna.Application.DTOs.Categories;

namespace SouQna.Application.Features.Categories.Queries.GetCategory
{
    public record GetCategoryQuery(
        Guid Id
    ) : IRequest<CategoryDTO>;
}