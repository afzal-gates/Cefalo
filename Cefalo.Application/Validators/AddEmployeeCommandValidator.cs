using FluentValidation;
using Cefalo.Application.Commands;

namespace Cefalo.Application.Validators;

public class AddEmployeeCommandValidator : AbstractValidator<AddEmployeeCommand>
{
    public AddEmployeeCommandValidator()
    {
        //RuleFor(o => o.UserName)
        //    .NotEmpty()
        //    .WithMessage("{UserName} is required")
        //    .NotNull()
        //    .MaximumLength(70)
        //    .WithMessage("{UserName} must not exceed 70 characters");
        //RuleFor(o => o.TotalPrice)
        //    .NotEmpty()
        //    .WithMessage("{TotalPrice} is required.")
        //    .GreaterThan(-1)
        //    .WithMessage("{TotalPrice} should not be -ve.");
        RuleFor(o => o.Email)
            .NotEmpty()
            .WithMessage("{Email} is required");
        RuleFor(o => o.FirstName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{FirstName} is required");
        RuleFor(o => o.LastName)
            .NotEmpty()
            .NotNull()
            .WithMessage("{LastName} is required");
    }
}