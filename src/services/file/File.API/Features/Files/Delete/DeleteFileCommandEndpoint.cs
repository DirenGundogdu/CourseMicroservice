using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace File.API.Features.Files.Delete;

public static class DeleteFileCommandEndpoint
{
    public static RouteGroupBuilder DeleteFileGroupItemEndpoint(this RouteGroupBuilder group) {
        // group.MapDelete("/{fileName}", async (string fileName, IMediator mediator) =>
        //         (await mediator.Send(new DeleteFileCommand(fileName))).ToGenericResult())
        group.MapDelete("",async ([FromBody]DeleteFileCommand command,[FromServices]IMediator mediator) =>
                (await mediator.Send(command)).ToGenericResult())
            .WithName("DeleteFile")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError);

        return group;
    }
}