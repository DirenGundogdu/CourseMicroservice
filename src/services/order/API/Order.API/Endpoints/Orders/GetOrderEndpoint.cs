using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.GetOrder;
using Shared.Extensions;

namespace Order.API.Endpoints.Orders;

public static class GetOrderEndpoint
{
    public static RouteGroupBuilder GetOrdersGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapGet("/", async (IMediator mediator) => (await mediator.Send(new GetOrdersQuery())).ToGenericResult())
            .WithName("GetOrders")
            .MapToApiVersion(1, 0);

        return group;
    }
}