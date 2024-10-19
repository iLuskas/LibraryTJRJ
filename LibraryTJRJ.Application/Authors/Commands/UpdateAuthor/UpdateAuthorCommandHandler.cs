using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Authors.Commands.UpdateAuthor;

public class UpdateAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateAuthorCommand, Updated>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Updated>> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            return Errors.Author.NotFound;
        }

        author.Update(request.Name);

        await _authorRepository.UpdateAuthorAsync(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
