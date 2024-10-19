using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;

namespace LibraryTJRJ.Application.Authors.Queries.GetAuthor;

public record GetAuthorQuery(Guid AuthorId) : IQuery<Author>;


