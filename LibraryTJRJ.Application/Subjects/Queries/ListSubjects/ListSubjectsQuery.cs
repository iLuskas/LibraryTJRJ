using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Subjects.Queries.ListSubjects;

public record ListSubjectsQuery : IQuery<List<Subject>>;
