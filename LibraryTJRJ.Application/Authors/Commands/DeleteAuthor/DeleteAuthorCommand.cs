using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Authors.Commands.DeleteAuthor;

public record DeleteAuthorCommand(Guid AuthorId) : ICommand<Deleted>;
