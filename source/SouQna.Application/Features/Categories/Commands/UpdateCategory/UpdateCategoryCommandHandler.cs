using MediatR;
using SouQna.Application.Exceptions;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<UpdateCategoryCommand, UpdateCategoryResponse>
    {
        public async Task<UpdateCategoryResponse> Handle(
            UpdateCategoryCommand command,
            CancellationToken cancellationToken
        )
        {
            var category = await unitOfWork.Categories.FindAsync(
                c => c.Id == command.Id
            ) ?? throw new NotFoundException($"Category with ID {command.Id} not found");

            category.UpdateDetails(command.Name, command.Description);
            await unitOfWork.SaveChangesAsync();

            return new UpdateCategoryResponse
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
            };
        }
    }
}