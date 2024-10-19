using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;

namespace LibraryTJRJ.Application.Books.Queries.GetBook;

public record GetBookQuery(Guid BookId) : IQuery<Book>;


