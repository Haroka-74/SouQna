using MediatR;
using AutoMapper;
using SouQna.Application.Common;
using SouQna.Application.Interfaces;
using SouQna.Application.DTOs.Orders;

namespace SouQna.Application.Features.Orders.GetOrders
{
    public class GetOrdersRequestHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper
    ) : IRequestHandler<GetOrdersRequest, Page<OrderDTO>>
    {
        public async Task<Page<OrderDTO>> Handle(
            GetOrdersRequest request,
            CancellationToken cancellationToken
        )
        {
            var (items, totalCount) = await unitOfWork.Orders.GetPagedAsync(
                request.PageNumber,
                request.PageSize,
                o => o.OrderStatus == request.OrderStatus,
                q => q.OrderByDescending(o => o.CreatedAt)
            );

            var orders = mapper.Map<List<OrderDTO>>(items);

            return new Page<OrderDTO>(
                request.PageNumber,
                request.PageSize,
                totalCount,
                orders.AsReadOnly()
            );
        }
    }
}