using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Subjects.Commands.DeleteSubject;

public class DeleteSubjectCommandHandler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork) : ICommandHandler<DeleteSubjectCommand, Deleted>
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Deleted>> Handle(DeleteSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject is null)
        {
            return Errors.Subject.NotFound;
        }

        await _subjectRepository.RemoveSubjectAsync(subject);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Deleted;
    }
}
