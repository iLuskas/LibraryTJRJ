using FluentValidation;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public class ListBooksQueryValidator : AbstractValidator<ListBooksQuery>
{
    public ListBooksQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThan(0).WithMessage("PageNumber must greater than 0.");

        RuleFor(x => x.PageSize)
            .GreaterThan(0).WithMessage("PageSize must greater than 0.");
    }
}
