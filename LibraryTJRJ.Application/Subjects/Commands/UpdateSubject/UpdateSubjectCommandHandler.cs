using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Domain.Common.Errors;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Subjects.Commands.UpdateSubject;

public class UpdateSubjectCommandHandler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork) : ICommandHandler<UpdateSubjectCommand, Updated>
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Updated>> Handle(UpdateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = await _subjectRepository.GetByIdAsync(request.SubjectId);

        if (subject is null)
        {
            return Errors.Subject.NotFound;
        }

        subject.Update(request.Description);

        await _subjectRepository.UpdateSubjectAsync(subject);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Updated;
    }
}
