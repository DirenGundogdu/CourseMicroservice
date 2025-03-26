namespace Catalog.API.Features.Courses.Create;

public class CreateCourseCommandHandler(AppDbContext context,IMapper mapper) : IRequestHandler<CreateCourseCommand,ServiceResult<Guid>>
{

    public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken) {

        var hasCategory = await context.Categories.AnyAsync(x => x.Id == request.CategoryId, cancellationToken);

        if (!hasCategory)
        {
          return  ServiceResult<Guid>.Error("Category not found.",$"The Category with id({request.CategoryId}) does not exist.",HttpStatusCode.NotFound);
        }
        
        var hasCourse = await context.Courses.AnyAsync(x => x.Name == request.Name, cancellationToken);

        if (hasCourse)
        {
            return ServiceResult<Guid>.Error("Course already exists",$"Course with name '{request.Name}' already exists", HttpStatusCode.BadRequest);
        }
        
        var newCourse = mapper.Map<Course>(request);
        newCourse.Created = DateTime.Now;
        newCourse.Id = NewId.NextSequentialGuid();
        newCourse.Feature = new Feature() {
            Duration = 10,
            EducatorFullName = "John",
            Rating = 0
        };

        context.Courses.Add(newCourse);
        await context.SaveChangesAsync(cancellationToken);
        
        return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id,$"/api/courses/{newCourse.Id}");
    }
}