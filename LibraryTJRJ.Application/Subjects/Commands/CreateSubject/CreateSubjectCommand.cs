using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Subjects.Commands.CreateSubject;

public record CreateSubjectCommand(
    string Description) : ICommand<Subject>;
