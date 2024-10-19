using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateBookCommand, Updated>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Updated>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Errors.Book.NotFound;
        }

        book.Update(request.Title, request.Publisher, request.Edition, request.YearPublication);

        await _bookRepository.UpdateBookAsync(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
