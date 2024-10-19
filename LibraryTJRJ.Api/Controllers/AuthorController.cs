using Asp.Versioning;
using LibraryTJRJ.Application.Authors.Commands.CreateAuthor;
using LibraryTJRJ.Application.Authors.Commands.DeleteAuthor;
using LibraryTJRJ.Application.Authors.Commands.UpdateAuthor;
using LibraryTJRJ.Application.Authors.Queries.GetAuthor;
using LibraryTJRJ.Application.Authors.Queries.ListAuthors;
using LibraryTJRJ.Application.Authors.Queries.ListAuthorsByBook;
using LibraryTJRJ.Contracts.Authors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTJRJ.Api.Controllers;

[ApiVersion(ApiVersions.V1)]
[Route("api/v{version:apiVersion}/author")]
public class AuthorController(ISender sender) : ApiController
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] AuthorRequest request)
    {
        var createAuthorResult = await _sender.Send(new CreateAuthorCommand(request.Name));

        return createAuthorResult.Match(
            author => CreatedAtAction(
                nameof(GetAuthor),
                new { AuthorId = author.Id },
                new AuthorResponse(author.Id, author.Name)),
            Problem);
    }

    [HttpPut("{authorId:guid}")]
    public async Task<IActionResult> Update(Guid authorId, [FromBody] AuthorRequest request)
    {
        var command = new UpdateAuthorCommand(
                    AuthorId: authorId,
                    Name: request.Name
        );

        var result = await _sender.Send(command);

        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpDelete("{authorId:guid}")]
    public async Task<IActionResult> Delete(Guid authorId)
    {
        var command = new DeleteAuthorCommand(authorId);

        var result = await _sender.Send(command);

        return result.Match(
            _ => NoContent(),
            errors => Problem(errors)
        );
    }

    [HttpGet]
    public async Task<IActionResult> ListAuthors()
    {
        var listAuthorsResult = await _sender.Send(new ListAuthorsQuery());

        return listAuthorsResult.Match(
            authors => Ok(authors.ConvertAll(author => new AuthorResponse(author.Id, author.Name))),
            Problem);
    }

    [HttpGet("{authorId:guid}")]
    public async Task<IActionResult> GetAuthor(Guid authorId)
    {
        var command = new GetAuthorQuery(authorId);

        var getAuthorResult = await _sender.Send(command);

        return getAuthorResult.Match(
            author => Ok(new AuthorResponse(author.Id, author.Name)),
            Problem);
    }

    [HttpGet("{bookId:guid}/authors")]
    public async Task<IActionResult> GetAuthorsByBook(Guid bookId)
    {
        var command = new ListAuthorsByBookQuery(bookId);

        var getAuthorResult = await _sender.Send(command);

        return getAuthorResult.Match(
            authors => Ok(authors.ConvertAll(author =>
                new AuthorResponse(author.Id, author.Name))
            ),
            Problem);
    }
}
