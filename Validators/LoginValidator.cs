using BookTradeHubAPI.Models;
using FluentValidation;

namespace BookTradeHubAPI.Validators
{
    public class LoginValidator : AbstractValidator<LoginModel>
    {
        public LoginValidator()
        {
            RuleFor(l => l.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid");
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
