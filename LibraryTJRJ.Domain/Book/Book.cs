using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Domain.Book;

public class Book : Entity
{
    private readonly List<Guid> _authorIds = [];
    private readonly List<Guid> _subjectIds = [];

    public string Title { get; private set; } = null!;

    public string Publisher { get; private set; } = null!;

    public int Edition { get; private set; }

    public int YearPublication { get; private set; }

    public DateTime CreatedDateTime { get; }

    public DateTime UpdatedDateTime { get; }

    public IReadOnlyCollection<Guid> AuthorIds => _authorIds.AsReadOnly();

    public IReadOnlyCollection<Guid> SubjectIds => _subjectIds.AsReadOnly();

    private Book(string title, string publisher, int edition, int yearPublication, DateTime createdDateTime, DateTime updatedDateTime)
    {
        Title = title;
        Publisher = publisher;
        Edition = edition;
        YearPublication = yearPublication;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    public Book()
    {        
    }

    public static Book Create(string title, string publisher, int edition, int yearPublication)
    {
        return new(title, publisher, edition, yearPublication, DateTime.UtcNow, DateTime.UtcNow);
    }
}
