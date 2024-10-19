using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Domain.Authors;

public class Author : Entity
{
    private readonly List<Book> _books = [];

    public string Name { get; private set; } = null!;

    public DateTime CreatedDateTime { get; }   

    public DateTime UpdatedDateTime { get; private set; }

    public IReadOnlyCollection<Book> Books => _books.AsReadOnly();

    private Author(Guid id, string name, DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        Name = name;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Author()
    {     
    }

    public static Author Create(string name)
    {
        return new(Guid.NewGuid(), name, DateTime.UtcNow, DateTime.UtcNow);
    }

    public void Update(string name)
    {
        Name = name;
        UpdatedDateTime = DateTime.UtcNow;
    }
}
