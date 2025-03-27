namespace Catalog.API.Features.Courses.Update;

public static class UpdateCourseCommandEndpoint
{
    public static RouteGroupBuilder UpdateCourseGroupItemEndpoints(this RouteGroupBuilder group) {
        group.MapPut("/", async (UpdateCourseCommand command, IMediator Mediator) =>
                (await Mediator.Send(command)).ToGenericResult())
            .WithName("UpdateCourse").MapToApiVersion(1,0)
            .AddEndpointFilter<ValidationFilter<UpdateCourseCommand>>();
        
        return group;
    }
    
}