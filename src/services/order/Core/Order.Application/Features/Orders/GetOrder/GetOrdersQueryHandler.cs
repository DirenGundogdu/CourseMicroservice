using AutoMapper;
using MediatR;
using Order.Application.Contracts.Repositories;
using Order.Application.Features.Orders.Create;
using Shared;
using Shared.Services;

namespace Order.Application.Features.Orders.GetOrder;

public class GetOrdersQueryHandler(IIdentityService identityService,IOrderRepository orderRepository,IMapper mapper) : IRequestHandler<GetOrdersQuery, ServiceResult<List<GetOrdersResponse>>>
{

    public async Task<ServiceResult<List<GetOrdersResponse>>> Handle(GetOrdersQuery request, CancellationToken cancellationToken) {
        var orders = await orderRepository.GetOrderByBuyerId(identityService.GetUserId);

        var response = orders.Select(x => new GetOrdersResponse(x.Created, x.TotalPrice, mapper.Map<List<OrderItemDto>>(x.OrderItems))).ToList();

        return ServiceResult<List<GetOrdersResponse>>.SuccessAsOk(response);
    }
}