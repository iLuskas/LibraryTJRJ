using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Domain.Subjects;

public class Subject : Entity
{
    private readonly List<Book> _books = [];

    public string Description { get; private set; } = null!;
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; private set; }
    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    private Subject(Guid id, string description, DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        Description = description;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Subject() { }

    public static Subject Create(string description) =>
        new (Guid.NewGuid(), description, DateTime.UtcNow, DateTime.UtcNow);

    public void Update(string description)
    {
        Description = description;

        UpdatedDateTime = DateTime.UtcNow;
    }
}
