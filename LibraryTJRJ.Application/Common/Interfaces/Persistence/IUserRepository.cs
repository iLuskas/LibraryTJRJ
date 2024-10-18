using LibraryTJRJ.Domain.User;

namespace LibraryTJRJ.Application.Common.Interfaces.Persistence;

public interface IUserRepository
{
    User? GetUserByEmail(string email);
    void Add(User user);
}
