using Shared;

namespace Order.Application.Features.Orders.GetOrder;

public record GetOrdersQuery : IRequestByServiceResult<List<GetOrdersResponse>>;