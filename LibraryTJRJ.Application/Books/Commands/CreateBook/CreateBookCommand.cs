using LibraryTJRJ.Application.Common.Interfaces.Messaging;
using LibraryTJRJ.Domain.Books;

namespace LibraryTJRJ.Application.Books.Commands.CreateBook;

public record CreateBookCommand(
    string Title,
    string Publisher,
    int Edition,
    string YearPublication,
    List<Guid> AuthorIds,
    List<Guid> SubjectIds) : ICommand<Book>;
