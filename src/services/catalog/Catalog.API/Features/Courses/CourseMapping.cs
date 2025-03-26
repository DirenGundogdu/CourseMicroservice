using Catalog.API.Features.Courses.Create;
using Catalog.API.Features.Courses.DTOs;

namespace Catalog.API.Features.Courses;

public class CourseMapping:Profile
{
    public CourseMapping()
    {
        CreateMap<CreateCourseCommand, Course>();
        CreateMap<Course, CourseDto>().ReverseMap();
        CreateMap<Feature, FeatureDto>().ReverseMap();

    }
}