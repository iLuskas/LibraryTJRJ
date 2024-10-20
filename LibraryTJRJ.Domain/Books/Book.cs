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

    private Book(
        string title,
        string publisher,
        int edition,
        string yearPublication,
        List<Author> authors,
        List<Subject> subjects,
        DateTime createdDateTime,
        DateTime updatedDateTime)
    {
        Title = title;
        Publisher = publisher;
        Edition = edition;
        _authors.AddRange(authors);
        _subjects.AddRange(subjects);
        YearPublication = yearPublication;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Book()
    {        
    }

    public static Book Create(string title, string publisher, int edition, string yearPublication, List<Author> authors, List<Subject> subjects)
    {
        return new(title, publisher, edition, yearPublication, authors ?? [], subjects ?? [], DateTime.UtcNow, DateTime.UtcNow);
    }

    public void Update(string title, string publisher, int edition, string yearPublication, List<Author> authors, List<Subject> subjects)
    {
        Title = title;
        Publisher = publisher;
        Edition = edition;
        YearPublication = yearPublication;

        _authors.Clear();
        _authors.AddRange(authors);

        _subjects.Clear();
        _subjects.AddRange(subjects);

        UpdatedDateTime = DateTime.UtcNow;
    }

    public void AddAuthor(Author author)
    {
        if (!_authors.Contains(author))
        {
            _authors.Add(author);
        }
    }

    public void RemoveAuthor(Author author)
    {
        _authors.Remove(author);
    }

    public void AddSubject(Subject subject)
    {
        if (!_subjects.Contains(subject))
        {
            _subjects.Add(subject);
        }
    }

    public void RemoveSubject(Subject subject)
    {
        _subjects.Remove(subject);
    }
}
