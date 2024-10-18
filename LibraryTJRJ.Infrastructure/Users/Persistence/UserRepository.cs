using LibraryTJRJ.Application.Common.Interfaces.Persistence;
using LibraryTJRJ.Domain.User;
using LibraryTJRJ.Infrastructure.Common.Persistence;

namespace LibraryTJRJ.Infrastructure.Users.Persistence;

public class UserRepository(LibraryTJRJDbContext dbContext) : IUserRepository
{
    private readonly LibraryTJRJDbContext _dbContext = dbContext;

    private static readonly List<User> _user = new();

    public void Add(User user)
    {
        _user.Add(user);
    }

    public User? GetUserByEmail(string email)
    {
        return _user.SingleOrDefault(w => w.Email.Equals(email));
    }
}
