using MediatR;
using SouQna.Domain.Enums;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;

namespace SouQna.Application.Features.Orders.Warehouse.GetConfirmedOrders
{
    public class GetConfirmedOrdersRequestHandler(
        IUnitOfWork unitOfWork
    ) : IRequestHandler<GetConfirmedOrdersRequest, Page<DTOs.OrderDTO>>
    {
        public async Task<Page<DTOs.OrderDTO>> Handle(
            GetConfirmedOrdersRequest request,
            CancellationToken cancellationToken
        )
        {
            var (items, totalCount) = await unitOfWork.Orders.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                o => o.OrderStatus == OrderStatus.Confirmed,
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