namespace Catalog.API.Features.Courses.Delete;

public record DeleteCourseCommand(Guid Id) : IRequestByServiceResult;

public  class DeleteCourseHandler(AppDbContext context) : IRequestHandler<DeleteCourseCommand, ServiceResult>
{
    
    
    public async Task<ServiceResult> Handle(DeleteCourseCommand request, CancellationToken cancellationToken) {
        var hasCourse = await context.Courses.FindAsync([request.Id],cancellationToken:cancellationToken);

        if (hasCourse is null)
        {
            return ServiceResult.ErrorNotFound();
        }

        context.Courses.Remove(hasCourse);
        await context.SaveChangesAsync(cancellationToken);

        return ServiceResult.SuccessAsNoContent();
    }
}

public static class DeleteCourseEndpoint
{
    public static RouteGroupBuilder DeleteCourseGroupItemEndpoint(this RouteGroupBuilder group) {
        group.MapDelete("/{id:guid}", async (IMediator mediator, Guid id) =>
                (await mediator.Send(new DeleteCourseCommand(id))).ToGenericResult())
            .MapToApiVersion(1,0).WithName("DeleteCourse");

        return group;
    }
}