using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Domain.Common.Interfaces;

namespace LibraryTJRJ.Application.Subjects.Commands.CreateSubject;

public sealed class CreateSubjectCommandHandler(ISubjectRepository subjectRepository, IUnitOfWork unitOfWork) :
    ICommandHandler<CreateSubjectCommand, Subject>
{
    private readonly ISubjectRepository _subjectRepository = subjectRepository;
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ErrorOr<Subject>> Handle(CreateSubjectCommand request, CancellationToken cancellationToken)
    {
        var subject = Subject.Create(request.Description);

        await _subjectRepository.AddAsync(subject);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return subject;
    }
}
