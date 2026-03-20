using MediatR;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Products.DeleteProduct
{
    public class DeleteProductRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteProductRequest>
    {
        public async Task Handle(
            DeleteProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.Id
            );

            if(product is null)
                return;

            await unitOfWork.Products.DeleteAsync(product);
            await unitOfWork.SaveChangesAsync();
        }
    }
}