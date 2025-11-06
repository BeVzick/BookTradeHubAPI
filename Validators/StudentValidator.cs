using BookTradeHubAPI.Models.Entity;
using FluentValidation;

namespace BookTradeHubAPI.Validators;

public class StudentValidator : AbstractValidator<Student>
{
    public StudentValidator()
    {
        RuleFor(s => s.FirstName)
            .NotEmpty().WithMessage("Field 'FirstName' is required")
            .MaximumLength(100).WithMessage("Field 'FirstName' cannot be longer than 100 characters")
            .Matches(@"^[A-Z]{1}[a-z]+$").WithMessage("Field 'FirstName' is not valid");

        RuleFor(s => s.LastName)
            .NotEmpty().WithMessage("Field 'LastName' is required")
            .MaximumLength(100).WithMessage("Field 'LastName' cannot be longer than 100 characters")
            .Matches(@"^[A-Z]{1}[a-z]+$").WithMessage("Field 'LastName' is not valid");

        RuleFor(s => s.Age)
            .NotEmpty().WithMessage("Field 'Age' is required")
            .InclusiveBetween(16, 100).WithMessage("Field 'Age' is not valid");

        RuleFor(s => s.BookIds)
            .Empty().WithMessage("Field 'BookIds' must be empty");
    }
}
