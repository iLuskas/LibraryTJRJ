using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Subjects.Queries.ListSubjects;

public class ListSubjectsQueryHandler(ISubjectRepository subjectRepository) : IQueryHandler<ListSubjectsQuery, List<Subject>>
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;

    public async Task<ErrorOr<List<Subject>>> Handle(ListSubjectsQuery request, CancellationToken cancellationToken)
    {
        return await _subjectRepository.GetAllAsync();
    }
}
