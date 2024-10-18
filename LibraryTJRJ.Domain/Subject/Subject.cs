using LibraryTJRJ.Domain.Abstractions;

namespace LibraryTJRJ.Domain.Subject;

public class Subject : Entity
{
    public string Description { get; private set; } = null;

    private Subject(Guid id, string description) : base(id)
    {
        Description = description;
    }

    private Subject() { }

    public static Subject Create(string description) =>
        new (Guid.NewGuid(), description);
}
