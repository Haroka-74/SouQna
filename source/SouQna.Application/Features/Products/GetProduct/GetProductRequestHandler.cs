using MediatR;
using AutoMapper;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.Features.Products.Shared;

namespace SouQna.Application.Features.Products.GetProduct
{
    public class GetProductRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetProductRequest, ProductDTO>
    {
        public async Task<ProductDTO> Handle(
            GetProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.Id
            ) ?? throw new NotFoundException($"Product with (id: {request.Id}) was not found");

            return mapper.Map<ProductDTO>(product);
        }
    }
}