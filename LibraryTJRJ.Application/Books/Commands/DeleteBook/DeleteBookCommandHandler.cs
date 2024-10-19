using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteBookCommand, Deleted>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Errors.Book.NotFound;
        }

        await _bookRepository.RemoveBookAsync(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}
