using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Domain.Common.Errors;

namespace LibraryTJRJ.Application.Subjects.Queries.GetSubject;

public sealed class GetSubjectQueryHandler(ISubjectRepository subjectRepository) : IQueryHandler<GetSubjectQuery, Subject>
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;

    public async Task<ErrorOr<Subject>> Handle(GetSubjectQuery request, CancellationToken cancellationToken)
    {
        if (await _subjectRepository.GetByIdAsync(request.SubjectId) is not Subject subject)
        {
            return Errors.Subject.NotFound;
        }

        return subject;
    }
}
