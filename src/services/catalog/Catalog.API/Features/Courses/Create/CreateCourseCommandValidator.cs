namespace Catalog.API.Features.Courses.Create;

public class CreateCourseCommandValidator : AbstractValidator<CreateCourseCommand>
{
    public CreateCourseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("{PropertName} is required.")
            .MaximumLength(100).WithMessage("{PropertName} must not exceed 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("{PropertName} is required.")
            .MaximumLength(1000).WithMessage("{PropertName} must not exceed 1000 characters.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("{PropertName} must be greater than 0.");

        RuleFor(x => x.CategoryId)
            .NotEmpty().WithMessage("{PropertName} is required.");
    }
    
}