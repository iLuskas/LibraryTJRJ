namespace LibraryTJRJ.Contracts.Books;

public record BookRequest(
    string Title,
    string Publisher,
    int Edition,
    string YearPublication);
