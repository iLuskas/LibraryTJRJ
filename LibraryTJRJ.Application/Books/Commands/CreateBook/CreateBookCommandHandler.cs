using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Interfaces;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Books.Commands.CreateBook;

public sealed class CreateBookCommandHandler(IBookRepository bookRepository,
                                             IAuthorRepository AuthorRepository,
                                             ISubjectRepository SubjectRepository,
                                             IUnitOfWork unitOfWork) :
    ICommandHandler<CreateBookCommand, Book>
{
    private readonly IBookRepository _bookRepository = bookRepository;
    private readonly IAuthorRepository _authorRepository = AuthorRepository;
    private readonly ISubjectRepository _subjectRepository = SubjectRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Book>> Handle(CreateBookCommand request, CancellationToken cancellationToken)
    {
        List<Author> authors = new();
        List<Subject> subjects = new();

        if (request.AuthorIds.Any())
            authors = await _authorRepository.GetByIdsAsync(request.AuthorIds);

        if (request.SubjectIds.Any())
            subjects = await _subjectRepository.GetByIdsAsync(request.SubjectIds);

        var book = Book.Create(
            request.Title,
            request.Publisher,
            request.Edition,
            request.YearPublication,
            authors,
            subjects);

        await _bookRepository.AddAsync(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return book;
    }
}
