using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Books.Commands.DeleteBook;

public record DeleteBookCommand(Guid BookId) : ICommand<Deleted>;
