using LibraryTJRJ.Domain.Common.Models;

namespace LibraryTJRJ.Domain.User;

public class User : Entity
{
    public string FirstName { get; } = null!;
    public string LastName { get; } = null!;
    public string Email { get; } = null!;
    public string Password { get; } = null!;
    public DateTime CreatedDateTime { get; }
    public DateTime UpdatedDateTime { get; }

    private User(Guid id, string firstName, string lastName, string email, string password, DateTime createdDateTime, DateTime updatedDateTime)
    : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Password = password;
        CreatedDateTime = createdDateTime;
        UpdatedDateTime = updatedDateTime;
    }

    private User()
    {
    }

    public static User Create(string firstName, string lastName, string email, string password)
    {
        return new(Guid.NewGuid(), firstName, lastName, email, password, DateTime.UtcNow, DateTime.UtcNow);
    }

}
