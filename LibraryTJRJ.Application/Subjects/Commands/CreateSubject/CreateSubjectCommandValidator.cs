using FluentValidation;

namespace LibraryTJRJ.Application.Subjects.Commands.CreateSubject;

public class CreateSubjectCommandValidator : AbstractValidator<CreateSubjectCommand>
{
    public CreateSubjectCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(20).WithMessage("Name must not exceed 20 characters.");
    }
}