using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Subjects.Queries.GetSubject;

public record GetSubjectQuery(Guid SubjectId) : IQuery<Subject>;


