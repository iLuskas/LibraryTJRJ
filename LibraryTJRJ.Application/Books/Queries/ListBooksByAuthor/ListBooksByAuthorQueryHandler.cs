using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;

namespace LibraryTJRJ.Application.Books.Queries.ListBooksByAuthor;

public sealed class ListBooksByAuthorQueryHandler(IAuthorRepository AuthorRepository, IBookRepository BookRepository) 
    : IQueryHandler<ListBooksByAuthorQuery, List<Book>>
{
    private readonly IAuthorRepository _authorRepository = AuthorRepository;
    private readonly IBookRepository _bookRepository = BookRepository;

    public async Task<ErrorOr<List<Book>>> Handle(ListBooksByAuthorQuery request, CancellationToken cancellationToken)
    {
        if (await _authorRepository.ExistsAsync(request.AuthorId))
        {
            return Errors.Author.NotFound;
        }

        return await _bookRepository.GetAllByAuthorAsync(request.AuthorId);
    }
}
