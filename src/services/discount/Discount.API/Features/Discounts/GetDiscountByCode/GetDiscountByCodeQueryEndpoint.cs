using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Discount.API.Features.Discounts.GetDiscountByCode;

public static class GetDiscountByCodeQueryEndpoint
{
    public static RouteGroupBuilder GetDiscountByCodeGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapGet("/{code:length(10)}", async (string code,IMediator mediator)=> (await mediator.Send(new GetDiscountByCodeQuery(code))).ToGenericResult())
            .WithName("GetDiscountByCode")
            .MapToApiVersion(1, 0)
            .Produces<GetDiscountByCodeQueryResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        return group;
    }
}