using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Authors;

namespace LibraryTJRJ.Application.Authors.Commands.CreateAuthor;

public record CreateAuthorCommand(string Name) : ICommand<Author>;
