using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Books.Commands.CreateBook;

public sealed class CreateBookCommandHandler(IBookRepository bookRepository, IUnitOfWork unitOfWork) :
    ICommandHandler<CreateBookCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        var book = Book.Create(request.Title, request.Publisher, request.Edition, request.YearPublication);

        await _bookRepository.AddAsync(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book;
    }
}
