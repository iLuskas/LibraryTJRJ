using LibraryTJRJ.Domain.User;
using LibraryTJRJ.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Users.Persistence;

public class UserRepository(LibraryTJRJDbContext dbContext) : IUserRepository
{
    private readonly LibraryTJRJDbContext _dbContext = dbContext;

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Users.AsNoTracking().AnyAsync(gym => gym.Id == id);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(gym => gym.Id == id);
    }

    public async Task<User?> GetUserByEmailAsync(string email)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(w => w.Email.Equals(email));
    }

    public Task RemoveUserAsync(User user)
    {
        _dbContext.Remove(user);

        return Task.CompletedTask;
    }

    public Task UpdateUserAsync(User user)
    {
        _dbContext.Update(user);

        return Task.CompletedTask;
    }
}
