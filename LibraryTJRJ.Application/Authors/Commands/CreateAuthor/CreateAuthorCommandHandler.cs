using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Authors.Commands.CreateAuthor;

public sealed class CreateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork) :
    ICommandHandler<CreateAuthorCommand, Author>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = Author.Create(request.Name);

        await _authorRepository.AddAsync(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return author;
    }
}
