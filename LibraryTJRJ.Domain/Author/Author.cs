using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Domain.Author;

public class Author : Entity
{
    private readonly List<Guid> _bookIds = [];
 
    public string? Name { get; private set; }

    public DateTime CreatedDateTime { get; }   

    public DateTime UpdatedDateTime { get; }

    public IReadOnlyCollection<Guid> Books => _bookIds.AsReadOnly();

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
}
