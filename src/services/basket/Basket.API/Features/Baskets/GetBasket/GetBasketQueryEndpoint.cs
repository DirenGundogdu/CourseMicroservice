using MediatR;
using Shared.Extensions;

namespace Basket.API.Features.Baskets.GetBasket;

public static class GetBasketQueryEndpoint
{
    public static RouteGroupBuilder GetBasketItemGroupEndpoint(this RouteGroupBuilder group) {
        group.MapGet("/user", async (IMediator mediator) => (await mediator.Send(new GetBasketQuery())).ToGenericResult())
            .WithName("GetBasket")
            .MapToApiVersion(1,0);

        return group;
    }
    
    
}