using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public class ListBooksQueryHandler(IBookRepository authorRepository) : IQueryHandler<ListBooksQuery, List<Book>>
{
    private readonly IBookRepository _authorRepository = authorRepository;

    public async Task<ErrorOr<List<Book>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAsync();
    }
}
