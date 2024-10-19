using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Subjects.Commands.UpdateSubject;

public record UpdateSubjectCommand(Guid SubjectId, string Description) : ICommand<Updated>;


