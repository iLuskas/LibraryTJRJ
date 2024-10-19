using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;

namespace LibraryTJRJ.Application.Books.Queries.GetBook;

public sealed class GetBookQueryHandler(IBookRepository bookRepository) : IQueryHandler<GetBookQuery, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<ErrorOr<Book>> Handle(GetBookQuery request, CancellationToken cancellationToken)
    {
        if (await _bookRepository.GetByIdAsync(request.BookId) is not Book book)
        {
            return Errors.Book.NotFound;
        }

        return book;
    }
}
