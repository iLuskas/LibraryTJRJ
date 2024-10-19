using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public record ListBooksQuery : IQuery<List<Book>>;
