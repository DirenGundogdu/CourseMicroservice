namespace Discount.API.Features.Discounts.Create;

public class CreateDiscountCommandValidator : AbstractValidator<CreateDiscountCommand>
{
    
    public CreateDiscountCommandValidator()
    {
        RuleFor(x => x.Code)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .MaximumLength(50)
            .WithMessage("Code must not exceed 50 characters");

        RuleFor(x => x.Rate)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .WithMessage("Rate must be a valid number");

        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("{PropertyName} is required");

        RuleFor(x => x.Expired)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .GreaterThan(DateTime.UtcNow)
            .WithMessage("Expired date must be in the future");
    }


}