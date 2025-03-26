using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.Delete;
using Catalog.API.Features.Courses.GetAll;
using Catalog.API.Features.Courses.GetById;
using Catalog.API.Features.Courses.Update;

namespace Catalog.API.Features.Courses;

public static class CourseEndpointExt
{
    public static void AddCourseGroupEndpointExt(this WebApplication app) {
        app.MapGroup("api/courses")
            .WithTags("Courses")
            .CreateCourseGroupItemEndpoint()
            .GetAllCoursesGroupItemEndpoint()
            .GetCourseByIdGroupItemEndpoint()
            .UpdateCourseGroupItemEndpoints()
            .DeleteCourseGroupItemEndpoint();
    }
}