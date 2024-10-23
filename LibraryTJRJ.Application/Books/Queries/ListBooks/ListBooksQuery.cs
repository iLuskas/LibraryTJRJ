using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Contracts.Books;
using LibraryTJRJ.Contracts.Common;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public record ListBooksQuery(int PageNumber, int PageSize, string? SearchTerm, string? SortColumn, string? SortOrder) : IQuery<PagedResponse<BookResponse>>;
