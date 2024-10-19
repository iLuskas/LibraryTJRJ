using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;

namespace LibraryTJRJ.Application.Books.Queries.ListBooksByAuthor
{
    public record ListBooksByAuthorQuery(Guid AuthorId) : IQuery<List<Book>>;    
}
