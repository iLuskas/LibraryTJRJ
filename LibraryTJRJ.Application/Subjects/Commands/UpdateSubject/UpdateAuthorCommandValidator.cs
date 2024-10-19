using FluentValidation;

namespace LibraryTJRJ.Application.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommandValidator : AbstractValidator<UpdateSubjectCommand>
{
    public UpdateSubjectCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");
    }
}