using MediatR;
using SouQna.Domain.Enums;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.Warehouse.GetProcessingOrders
{
    public class GetProcessingOrdersRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetProcessingOrdersRequest, Page<DTOs.OrderDTO>>
    {
        public async Task<Page<DTOs.OrderDTO>> Handle(
            GetProcessingOrdersRequest request,
            CancellationToken cancellationToken
        )
        {
            var (items, totalCount) = await unitOfWork.Orders.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                o => o.OrderStatus == OrderStatus.Processing,
                q => q.OrderByDescending(o => o.CreatedAt)
            );

            var orders = items.Select(
                i => new DTOs.OrderDTO(
                    i.Id,
                    i.OrderNumber,
                    i.Total,
                    i.CreatedAt
                )
            ).ToList();

            return new Page<DTOs.OrderDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                orders.AsReadOnly()
            );
        }
    }
}