using FluentValidation;

namespace LibraryTJRJ.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
{
    public UpdateAuthorCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");
    }
}