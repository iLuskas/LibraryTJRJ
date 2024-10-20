using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public record ListBooksQuery(int PageNumber, int PageSize) : IQuery<PagedResponseOffset<Book>>;
