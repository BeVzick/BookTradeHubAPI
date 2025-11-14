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
                .EmailAddress();
            RuleFor(l => l.Password)
                .NotEmpty().WithMessage("Password is required");
        }
    }
}
