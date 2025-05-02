using Order.Application.Features.Orders.Create;

namespace Order.Application.Features.Orders.GetOrder;

public record GetOrdersResponse(DateTime Created,decimal TotalPrice, List<OrderItemDto> Items);