using MediatR;
using Shared.Extensions;
using Shared.Filters;

namespace Basket.API.Features.Baskets.AddBasketItem;

public static class AddBasketItemEndpoint
{
    public static RouteGroupBuilder AddBasketItemGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapPost("/item", async (AddBasketItemCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("AddBasketItem")
            .MapToApiVersion(1, 0)
            .AddEndpointFilter<ValidationFilter<AddBasketItemCommand>>();
        
        return group;
    }
    
}