using FluentValidation;

namespace Basket.API.Features.Baskets.AddBasketItem;

public class AddBasketItemCommandValidator : AbstractValidator<AddBasketItemCommand>
{
    public AddBasketItemCommandValidator() {
        
        RuleFor(x => x.CourseId)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotEqual(Guid.Empty).WithMessage("{PropertyName} is required.");

        RuleFor(x => x.CourseName)
            .NotEmpty().WithMessage("{PropertyName} is required.");

        RuleFor(x => x.CoursePrice)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .GreaterThan(0).WithMessage("{PropertyName} must be greater than 0.");

    }
}