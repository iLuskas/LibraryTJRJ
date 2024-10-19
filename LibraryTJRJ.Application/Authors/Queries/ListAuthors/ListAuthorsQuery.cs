using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;

namespace LibraryTJRJ.Application.Authors.Queries.ListAuthors;

public record ListAuthorsQuery : IQuery<List<Author>>;
