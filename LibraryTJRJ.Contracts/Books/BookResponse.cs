namespace LibraryTJRJ.Contracts.Books;

public record BookResponse(
    Guid Id, 
    string Title,
    string Publisher,
    int Edition,
    string YearPublication);
