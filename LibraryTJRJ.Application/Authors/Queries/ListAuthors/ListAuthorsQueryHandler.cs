using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;

namespace LibraryTJRJ.Application.Authors.Queries.ListAuthors;

public class ListAuthorsQueryHandler(IAuthorRepository authorRepository) : IQueryHandler<ListAuthorsQuery, List<Author>>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;

    public async Task<ErrorOr<List<Author>>> Handle(ListAuthorsQuery request, CancellationToken cancellationToken)
    {
        return await _authorRepository.GetAllAsync();
    }
}
