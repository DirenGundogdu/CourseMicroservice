using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Features.Discounts.Create;

public static class CreateDiscountCommandEndpoint
{
    public static RouteGroupBuilder CreateDiscountGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapPost("/", async (CreateDiscountCommand command, IMediator mediator) => (await mediator.Send(command)).ToGenericResult())
            .WithName("CreateDiscount")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .AddEndpointFilter<ValidationFilter<CreateDiscountCommandValidator>>();
        return group;
    }
}