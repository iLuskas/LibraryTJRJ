using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Common.Errors;

namespace LibraryTJRJ.Application.Authors.Queries.GetAuthor;

public sealed class GetAuthorQueryHandler(IAuthorRepository authorRepository) : IQueryHandler<GetAuthorQuery, Author>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<ErrorOr<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken)
    {
        if (await _authorRepository.GetByIdAsync(request.AuthorId) is not Author author)
        {
            return Errors.Author.NotFound;
        }

        return author;
    }
}
