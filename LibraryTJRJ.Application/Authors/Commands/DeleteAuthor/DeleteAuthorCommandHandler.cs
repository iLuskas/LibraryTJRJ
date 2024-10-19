using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Authors.Commands.DeleteAuthor;

public class DeleteAuthorCommandHandler(IAuthorRepository authorRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteAuthorCommand, Deleted>
{
    private readonly IAuthorRepository _authorRepository = authorRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
    {
        var author = await _authorRepository.GetByIdAsync(request.AuthorId);

        if (author is null)
        {
            return Errors.Author.NotFound;
        }

        await _authorRepository.RemoveAuthorAsync(author);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}
