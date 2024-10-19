using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;

namespace LibraryTJRJ.Application.Authors.Queries.ListAuthorsByBook;

public record ListAuthorsByBookQueryHandler(IAuthorRepository AuthorRepository, IBookRepository BookRepository)
    : IQueryHandler<ListAuthorsByBookQuery, List<Author>>
{
    private readonly IAuthorRepository _authorRepository = AuthorRepository;
    private readonly IBookRepository _bookRepository = BookRepository;
    public async Task<ErrorOr<List<Author>>> Handle(ListAuthorsByBookQuery request, CancellationToken cancellationToken)
    {
        if (await _bookRepository.ExistsAsync(request.BookId))
        {
            return Errors.Book.NotFound;
        }

        return await _authorRepository.GetAllByBookAsync(request.BookId);
    }
}
