using MediatR;
using Microsoft.AspNetCore.Mvc;
using Order.Application.Features.Orders.Create;
using Shared.Extensions;
using Shared.Filters;

namespace Order.API.Endpoints.Orders;

public static class CreateOrderEndpoint
{
    public static RouteGroupBuilder CreateOrderGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapPost("/", async ([FromBody]CreateOrderCommand command, [FromServices]IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateOrder")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<CreateOrderCommand>>();

        return group;
    }
}