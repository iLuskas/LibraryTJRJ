namespace LibraryTJRJ.Domain.User;

public interface IUserRepository
{
    Task<User?> GetUserByEmailAsync(string email);
    Task AddAsync(User user);
    Task<User?> GetByIdAsync(Guid id);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateUserAsync(User user);
    Task RemoveUserAsync(User user);
}
