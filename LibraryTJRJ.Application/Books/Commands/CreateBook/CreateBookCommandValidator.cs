using FluentValidation;

namespace LibraryTJRJ.Application.Books.Commands.CreateBook;

public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
{
    public CreateBookCommandValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title is required.")
            .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");

        RuleFor(x => x.Publisher)
            .NotEmpty().WithMessage("Publisher is required.")
            .MaximumLength(40).WithMessage("Name must not exceed 40 characters.");

        RuleFor(x => x.Edition)
            .GreaterThan(0).WithMessage("Edition must greater than 0.");

        RuleFor(x => x.YearPublication)
            .NotEmpty().WithMessage("YearPublication is required.")
            .MaximumLength(4).WithMessage("Name must not exceed 4 characters.")
            .Matches(@"^\d{4}$").WithMessage("Year of publication must be a valid year.");
    }
}