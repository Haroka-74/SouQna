using MediatR;

namespace SouQna.Application.Features.Categories.Commands.AddCategory
{
    public record AddCategoryCommand(
        string Name,
        string Description,
        Guid? ParentId
    ) : IRequest<AddCategoryResponse>;
}