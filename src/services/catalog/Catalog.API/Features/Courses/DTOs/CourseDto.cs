namespace Catalog.API.Features.Courses.DTOs;

public record CourseDto(Guid Id, string Name, string Description, decimal Price, string? ImageUrl, CategoryDto Category, FeatureDto Feature);