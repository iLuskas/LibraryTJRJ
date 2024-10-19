using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Subjects;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LibraryTJRJ.Application.Subjects.Queries.ListSubjectsByBook;

public class ListSubjectsByBookQueryHandler(ISubjectRepository SubjectRepository, IBookRepository BookRepository)
    : IQueryHandler<ListSubjectsByBookQuery, List<Subject>>
{
    private readonly ISubjectRepository _subjectRepository = SubjectRepository;
    private readonly IBookRepository _bookRepository = BookRepository;

    public async Task<ErrorOr<List<Subject>>> Handle(ListSubjectsByBookQuery request, CancellationToken cancellationToken)
    {
        if (await _bookRepository.ExistsAsync(request.BookId))
        {
            return Errors.Book.NotFound;
        }

        return await _subjectRepository.GetAllByBookAsync(request.BookId);
    }
}
