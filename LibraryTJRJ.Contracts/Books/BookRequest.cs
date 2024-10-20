namespace LibraryTJRJ.Contracts.Books;

public record BookRequest(
    string Title,
    string Publisher,
    int Edition,
    string YearPublication,
    List<Guid> AuthorIds,
    List<Guid> SubjectIds);
