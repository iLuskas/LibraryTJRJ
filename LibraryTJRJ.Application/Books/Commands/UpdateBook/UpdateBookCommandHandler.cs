using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Books.Commands.UpdateBook;

public class UpdateBookCommandHandler(IBookRepository BookRepository,
                                      IAuthorRepository AuthorRepository,
                                      ISubjectRepository SubjectRepository,
                                      IUnitOfWork UnitOfWork) : ICommandHandler<UpdateBookCommand, Updated>
{
    private readonly IBookRepository _bookRepository = BookRepository;
    private readonly IAuthorRepository _authorRepository = AuthorRepository;
    private readonly ISubjectRepository _subjectRepository = SubjectRepository;
    private readonly IUnitOfWork _unitOfWork = UnitOfWork;

    public async Task<ErrorOr<Updated>> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
    {
        var book = await _bookRepository.GetByIdAsync(request.BookId);

        if (book is null)
        {
            return Errors.Book.NotFound;
        }

        List<Author> authors = new();
        List<Subject> subjects = new();

        if (request.AuthorIds.Any())
            authors = await _authorRepository.GetByIdsAsync(request.AuthorIds);

        if (request.SubjectIds.Any())
            subjects = await _subjectRepository.GetByIdsAsync(request.SubjectIds);

        book.Update(
            title: request.Title,
            publisher: request.Publisher,
            edition: request.Edition,
            yearPublication: request.YearPublication,
            authors: authors,
            subjects: subjects
        );

        await _bookRepository.UpdateBookAsync(book);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
