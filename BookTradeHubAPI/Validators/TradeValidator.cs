
using FluentValidation;
using BookTradeHubAPI.Models.DTO.Create;

namespace BookTradeHubAPI.Validators;

public class TradeValidator : AbstractValidator<TradeCreateDto>
{
    public TradeValidator()
    {
        RuleFor(t => t.Student1Id)
            .NotEmpty().WithMessage("Field 'Student1Id' is required");

        RuleFor(t => t.Student2Id)
            .NotEmpty().WithMessage("Field 'Student2Id' is required");

        RuleFor(t => new { t.newStudent1BookIds, t.newStudent2BookIds })
            .Must(obj => obj.newStudent1BookIds.Count != 0 || obj.newStudent2BookIds.Count != 0)
            .WithMessage($"One of new Students bookIds must be not empty");
    }
}
