using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Common.Models;
using LibraryTJRJ.Domain.Subjects;

namespace LibraryTJRJ.Domain.Books;

public class Book : Entity
{
    private readonly List<Author> _authors = [];
    private readonly List<Subject> _subjects = [];

    public string Title { get; private set; } = null!;

    public string Publisher { get; private set; } = null!;

    public int Edition { get; private set; }

    public string YearPublication { get; private set; } = null!;

    public DateTime CreatedDateTime { get; }

    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyCollection<Author> Authors => _authors.AsReadOnly();

    public IReadOnlyCollection<Subject> Subjects => _subjects.AsReadOnly();

    private Book(string title, string publisher, int edition, string yearPublication, DateTime createdDateTime, DateTime updatedDateTime)
    {
        Title = title;
        Publisher = publisher;
        Edition = edition;
        YearPublication = yearPublication;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Book()
    {        
    }

    public static Book Create(string title, string publisher, int edition, string yearPublication)
    {
        return new(title, publisher, edition, yearPublication, DateTime.UtcNow, DateTime.UtcNow);
    }

    public void Update(string title, string publisher, int edition, string yearPublication)
    {
        Title = title;
        Publisher = publisher;
        Edition = edition;
        YearPublication = yearPublication;
        UpdatedDateTime = DateTime.UtcNow;
    }
}
