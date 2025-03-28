﻿using ErrorOr;
using LibraryTJRJ.Application.Common.Interfaces.Messaging;

namespace LibraryTJRJ.Application.Books.Commands.UpdateBook;

public record UpdateBookCommand(
    Guid BookId, 
    string Title,
    string Publisher,
    int Edition,
    string YearPublication,
    List<Guid> AuthorIds,
    List<Guid> SubjectIds) : ICommand<Updated>;


