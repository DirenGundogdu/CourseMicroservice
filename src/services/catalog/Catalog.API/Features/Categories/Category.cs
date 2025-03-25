using Catalog.API.Features.Courses;
using Catalog.API.Repositories;

namespace Catalog.API.Features.Categories;

public class Category : BaseEntity
{
    public string Name { get; set; } = default!;
    public List<Course>? Courses { get; set; }
}