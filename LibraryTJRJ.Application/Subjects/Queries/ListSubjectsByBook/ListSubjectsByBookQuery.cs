using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Application.Subjects.Queries.ListSubjectsByBook;

public record ListSubjectsByBookQuery(Guid BookId) : IQuery<List<Subject>>;