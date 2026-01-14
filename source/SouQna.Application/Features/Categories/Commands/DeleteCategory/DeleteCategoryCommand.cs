using MediatR;

namespace SouQna.Application.Features.Categories.Commands.DeleteCategory
{
    public record DeleteCategoryCommand(
        Guid Id
    ) : IRequest;
}