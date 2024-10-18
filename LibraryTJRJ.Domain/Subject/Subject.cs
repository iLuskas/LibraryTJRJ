using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Domain.Subject;

public class Subject : Entity
{
    public string Description { get; private set; } = null!;
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private Subject(Guid id, string description, DateTime createdDateTime, DateTime updatedDateTime) : base(id)
    {
        Description = description;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private Subject() { }

    public static Subject Create(string description) =>
        new (Guid.NewGuid(), description, DateTime.UtcNow, DateTime.UtcNow);
}
