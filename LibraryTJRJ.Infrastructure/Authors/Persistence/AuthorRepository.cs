using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Authors.Persistence;

public class AuthorRepository(LibraryTJRJDbContext dbContext) : IAuthorRepository
{
    private readonly LibraryTJRJDbContext _dbContext = dbContext;

    public async Task AddAsync(Author author)
    {
        await _dbContext.Authors.AddAsync(author);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Authors.AsNoTracking().AnyAsync(author => author.Id == id);
    }

    public async Task<List<Author>> GetAllAsync()
    {
        return await _dbContext.Authors.ToListAsync();
    }

    public async Task<List<Author>> GetAllByBookAsync(Guid bookId)
    {
        return await _dbContext.Authors
           .Where(author => author.Books.Any(book => book.Id == bookId))
           .Include(book => book.Books)
           .ToListAsync();
    }

    public async Task<Author?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Authors.FirstOrDefaultAsync(author => author.Id == id);
    }

    public async Task<List<Author>> GetByIdsAsync(List<Guid> authorIds)
    {
        return await _dbContext.Authors
            .Where(author => authorIds.Contains(author.Id))
            .ToListAsync();
    }

    public Task RemoveAuthorAsync(Author author)
    {
        _dbContext.Authors.Remove(author);

        return Task.CompletedTask;
    }

    public Task UpdateAuthorAsync(Author author)
    {
        _dbContext.Authors.Update(author);

        return Task.CompletedTask;
    }
}
