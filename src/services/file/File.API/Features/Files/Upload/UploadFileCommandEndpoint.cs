using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Extensions;

namespace File.API.Features.Files.Upload;

public static class UploadFileCommandEndpoint
{
    public static RouteGroupBuilder UploadFileCommandGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapPost("/", async (IFormFile file, IMediator mediator) => (await mediator.Send(new UploadFileCommand(file))).ToGenericResult())
            .WithName("UploadFile")
            .MapToApiVersion(1, 0)
            .Produces<Guid>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .DisableAntiforgery();
        return group;
    }
}