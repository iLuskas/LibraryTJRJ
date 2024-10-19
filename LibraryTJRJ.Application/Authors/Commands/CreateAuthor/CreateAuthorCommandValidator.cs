using FluentValidation;

namespace LibraryTJRJ.Application.Authors.Commands.CreateAuthor;

public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
{
    public CreateAuthorCommandValidator()
    {
        RuleFor(a => a.Name)
            .NotEmpty().WithMessage("Name is required.")
            .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");
    }
}