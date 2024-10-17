using LibraryTJRJ.Domain.Abstractions;

namespace LibraryTJRJ.Domain.Users;

public class User : Entity
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public string Password { get; } = null!;
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private User(Guid id, string firstName, string lastName, string email)
    : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
    }

    private User()
    {
    }
}
