using Asp.Versioning;
using LibraryTJRJ.Application.Books.Commands.CreateBook;
using LibraryTJRJ.Application.Books.Commands.DeleteBook;
using LibraryTJRJ.Application.Books.Commands.UpdateBook;
using LibraryTJRJ.Application.Books.Queries.GetBook;
using LibraryTJRJ.Application.Books.Queries.ListBooks;
using LibraryTJRJ.Application.Books.Queries.ListBooksByAuthor;
using LibraryTJRJ.Contracts.Books;
using LibraryTJRJ.Contracts.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTJRJ.Api.Controllers
{
    [ApiVersion(ApiVersions.V1)]
    [Route("api/v{version:apiVersion}/book")]
    public class BookController(ISender sender) : ApiController
    {
        private readonly ISender _sender = sender;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] BookRequest request)
        {
            var createBookResult = await _sender.Send(
                new CreateBookCommand(
                    request.Title,
                    request.Publisher,
                    request.Edition,
                    request.YearPublication,
                    request.AuthorIds,
                    request.SubjectIds
                )
            );

            return createBookResult.Match(
                book => CreatedAtAction(
                    nameof(GetBook),
                    new { BookId = book.Id },
                    new BookResponse(
                        book.Id,
                        book.Title,
                        book.Publisher,
                        book.Edition,
                        book.YearPublication,
                        book.Authors.Select(a => a.Name).ToList(),
                        book.Subjects.Select(s => s.Description).ToList())),
                Problem);
        }

        [HttpPut("{bookId:guid}")]
        public async Task<IActionResult> Update(Guid bookId, [FromBody] BookRequest request)
        {
            var command = new UpdateBookCommand(
                        BookId: bookId,
                        request.Title,
                        request.Publisher,
                        request.Edition,
                        request.YearPublication,
                        request.AuthorIds,
                        request.SubjectIds
            );

            var result = await _sender.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpDelete("{bookId:guid}")]
        public async Task<IActionResult> Delete(Guid bookId)
        {
            var command = new DeleteBookCommand(bookId);

            var result = await _sender.Send(command);

            return result.Match(
                _ => NoContent(),
                errors => Problem(errors)
            );
        }

        [HttpGet]
        public async Task<IActionResult> ListBooks([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 1)
        {
            var listBooksResult = await _sender.Send(new ListBooksQuery(pageNumber, pageSize));

            return listBooksResult.Match(
                 books =>
                 {
                     var totalRecords = books.TotalRecords;
                     var totalPages = books.TotalPages;

                     var pagedResponse = new PagedResponse<BookResponse>
                     {
                         PageNumber = pageNumber,
                         PageSize = pageSize,
                         TotalRecords = totalRecords,
                         TotalPages = totalPages,
                         Data = books.Data.ConvertAll(book => new BookResponse(
                             book.Id,
                             book.Title,
                             book.Publisher,
                             book.Edition,
                             book.YearPublication,
                             book.Authors.Select(a => a.Name).ToList(),
                             book.Subjects.Select(s => s.Description).ToList()))
                     };

                     return Ok(pagedResponse);
                 },
                 Problem);
        }

        [HttpGet("{bookId:guid}")]
        public async Task<IActionResult> GetBook(Guid bookId)
        {
            var command = new GetBookQuery(bookId);

            var getBookResult = await _sender.Send(command);

            return getBookResult.Match(
                book => Ok(new BookResponse(
                    book.Id,
                    book.Title,
                    book.Publisher,
                    book.Edition,
                    book.YearPublication,
                    book.Authors.Select(a => a.Name).ToList(),
                    book.Subjects.Select(s => s.Description).ToList())),
                Problem);
        }

        [HttpGet("{authorId}/books")]
        public async Task<IActionResult> GetBooksByAuthor(Guid authorId)
        {
            var booksResult = await _sender.Send(new ListBooksByAuthorQuery(authorId));

            return booksResult.Match(
                books => Ok(books.ConvertAll(book => 
                    new BookResponse(
                        book.Id,
                        book.Title,
                        book.Publisher,
                        book.Edition,
                        book.YearPublication,
                        book.Authors.Select(a => a.Name).ToList(),
                        book.Subjects.Select(s => s.Description).ToList()))
                ),
                Problem);
        }
    }
}
