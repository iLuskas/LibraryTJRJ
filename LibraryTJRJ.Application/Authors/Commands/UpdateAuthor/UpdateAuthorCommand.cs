using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Authors.Commands.UpdateAuthor;

public record UpdateAuthorCommand(Guid AuthorId, string Name) : ICommand<Updated>;


