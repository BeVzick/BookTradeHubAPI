using BookTradeHubAPI.Data;
using BookTradeHubAPI.Models.Entity;
using FluentValidation;

namespace BookTradeHubAPI.Validators;

public class BookValidator : AbstractValidator<Book>
{
    public BookValidator()
    {
        RuleFor(b => b.Title)
            .NotEmpty().WithMessage("Field 'Title' is required")
            .MaximumLength(100).WithMessage("Field 'Title' cannot be longer than 100 characters");

        RuleFor(b => b.Author)
            .NotEmpty().WithMessage("Field 'Author' is required")
            .Matches(@"^[A-Z]{1}[a-z]+\s[A-Z]{1}[a-z]+$").WithMessage("Field 'Author' is not valid")
            .MaximumLength(100).WithMessage("Field 'Author' cannot be longer than 100 characters");

        RuleFor(b => b.OwnerId)
            .NotEmpty().WithMessage("Filed 'OwnerId' is required")
            .GreaterThanOrEqualTo(0).WithMessage("Filed 'OwnerId' cannot be negative")
            .Must(id => StudentData.Students.Any(s => s.Id == id)).WithMessage($"Student doesn't exist");
    }
}
