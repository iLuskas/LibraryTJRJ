using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;

namespace LibraryTJRJ.Application.Authors.Queries.ListAuthorsByBook;

public record ListAuthorsByBookQuery(Guid BookId) : IQuery<List<Author>>;