using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Subjects.Commands.DeleteSubject;

public record DeleteSubjectCommand(Guid SubjectId) : ICommand<Deleted>;
