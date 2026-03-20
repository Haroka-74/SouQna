using MediatR;
using AutoMapper;
using SouQna.Application.Interfaces;
using SouQna.Application.Exceptions;
using SouQna.Application.DTOs.Products;

namespace SouQna.Application.Features.Products.GetProduct
{
    public class GetProductRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetProductRequest, AdminProductDTO>
    {
        public async Task<AdminProductDTO> Handle(
            GetProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await unitOfWork.Products.FindAsync(
                p => p.Id == request.Id,
                p => p.Inventory
            ) ?? throw new NotFoundException($"Product with (id: {request.Id}) was not found");

            return mapper.Map<AdminProductDTO>(product);
        }
    }
}