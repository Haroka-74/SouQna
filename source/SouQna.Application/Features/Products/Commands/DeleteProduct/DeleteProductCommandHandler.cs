using MediatR;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(
        IUnitOfWork unitOfWork,
        IFileService fileService
    ) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(
            DeleteProductCommand command,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == command.Id
            );

            if(product is null)
                return;

            await unitOfWork.Products.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();

            await fileService.DeleteFileAsync(product.Image);
        }
    }
}