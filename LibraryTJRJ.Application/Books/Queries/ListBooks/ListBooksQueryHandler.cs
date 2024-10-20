using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public class ListBooksQueryHandler(IBookRepository authorRepository) : IQueryHandler<ListBooksQuery, PagedResponseOffset<Book>>
{
    private readonly IBookRepository _authorRepository = authorRepository;

    public async Task<ErrorOr<PagedResponseOffset<Book>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetWithOffsetPagination(request.PageNumber, request.PageSize);
    }
}
