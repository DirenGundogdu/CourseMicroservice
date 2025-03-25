using Catalog.API.Features.Courses.Create;

namespace Catalog.API.Features.Courses;

public class CourseMapping:Profile
{
    
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>();
    }
}