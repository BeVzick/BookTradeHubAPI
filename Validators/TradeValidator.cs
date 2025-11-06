using BookTradeHubAPI.Data;
using FluentValidation;
using BookTradeHubAPI.Models.Entity;

namespace BookTradeHubAPI.Validators;

public class TradeValidator : AbstractValidator<Trade>
{
    public TradeValidator()
    {
        RuleFor(t => t.Student1Id)
            .NotEmpty().WithMessage("Field 'Student1Id' is required")
            .Must(id => StudentData.Students.Any(s => s.Id == id)).WithMessage($"Student1Id doesn't exist");

        RuleFor(t => t.Student2Id)
            .NotEmpty().WithMessage("Field 'Student2Id' is required")
            .Must(id => StudentData.Students.Any(s => s.Id == id)).WithMessage($"Student2Id doesn't exist");

        RuleFor(t => new { Student = t.Student1Id, BookIds = t.newStudent2BookIds })
            .Must(obj =>
                obj.BookIds.All(id => {
                    Student? student = StudentData.Students.FirstOrDefault(s => s.Id == obj.Student);
                    if (student == null)
                        return false;

                    return student.BookIds.Contains(id);
                })
            ).WithMessage("Student1Id doesn't have newStudent2BookIds");

        RuleFor(t => new { Student = t.Student2Id, BookIds = t.newStudent1BookIds })
            .Must(obj =>
                obj.BookIds.All(id => {
                    Student? student = StudentData.Students.FirstOrDefault(s => s.Id == obj.Student);
                    if (student == null)
                        return false;

                    return student.BookIds.Contains(id);
                })
            ).WithMessage("Student2Id doesn't have newStudent1BookIds");
    }
}
