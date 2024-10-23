using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Contracts.Books;
using LibraryTJRJ.Contracts.Common;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;
using System.Linq.Expressions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryTJRJ.Application.Books.Queries.ListBooks;

public class ListBooksQueryHandler(IBookRepository bookRepository) : IQueryHandler<ListBooksQuery, PagedResponse<BookResponse>>
{
    private readonly IBookRepository _bookRepository = bookRepository;

    public async Task<ErrorOr<PagedResponse<BookResponse>>> Handle(ListBooksQuery request, CancellationToken cancellationToken)
    {
        var sortExpression = GetSortProperty(request);

        var bookPaginated = await _bookRepository.GetWithOffsetPagination(
            request.PageNumber,
            request.PageSize,
            request.SearchTerm,
            sortExpression,
            request?.SortOrder
        );

        if(!bookPaginated.Data.Any())
            return Errors.Book.NotFound;

        var totalRecords = bookPaginated.TotalRecords;
        var totalPages = bookPaginated.TotalPages;

        var bookResponses = bookPaginated.Data.ConvertAll(MapToBookResponse);

        var pagedResponse = new PagedResponse<BookResponse>
        {
            PageNumber = request!.PageNumber,
            PageSize = request.PageSize,
            TotalRecords = totalRecords,
            TotalPages = totalPages,
            Data = bookResponses
        };

        return pagedResponse;
    }

    private static Expression<Func<Book, object>> GetSortProperty(ListBooksQuery request)
    {
        return request.SortColumn?.ToLower() switch
        {
            "title" => book => book.Title,
            "publisher" => book => book.Publisher,
            _ => book => book.Id
        };
    }

    private static BookResponse MapToBookResponse(Book book)
    {
        return new BookResponse(
            book.Id,
            book.Title,
            book.Publisher,
            book.Edition,
            book.YearPublication,
            book.Authors.Select(a => a.Name).ToList(),
            book.Subjects.Select(s => s.Description).ToList()
        );
    }
}
