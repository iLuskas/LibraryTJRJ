using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Models;
using LibraryTJRJ.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Books.Persistence;

public class BookRepository(LibraryTJRJDbContext dbContext) : IBookRepository
{
    private readonly LibraryTJRJDbContext _dbContext = dbContext;

    public async Task AddAsync(Book book)
    {
        await _dbContext.Books.AddAsync(book);
    }

    public async Task<bool> ExistsAsync(Guid id)
    {
        return await _dbContext.Books.AsNoTracking().AnyAsync(book => book.Id == id);
    }

    public async Task<List<Book>> GetAllAsync()
    {
        return await _dbContext.Books
            .Include(book => book.Authors)
            .Include(book => book.Subjects)
            .ToListAsync();
    }

    public async Task<List<Book>> GetAllByAuthorAsync(Guid authorId)
    {
        return await _dbContext.Books
            .Where(book => book.Authors.Any(author => author.Id == authorId))
            .Include(book => book.Authors)
            .ToListAsync();
    }

    public async Task<Book?> GetByIdAsync(Guid id)
    {
        return await _dbContext.Books.FirstOrDefaultAsync(book => book.Id == id);
    }

    public async Task<PagedResponseOffset<Book>> GetWithOffsetPagination(int pageNumber, int pageSize)
    {
        var totalRecords = await _dbContext.Books.AsNoTracking().CountAsync();

        var entities = await _dbContext.Books.AsNoTracking()
            .Include(book => book.Authors)
            .Include(book => book.Subjects)
            .OrderBy(x => x.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        var pagedResponse = new PagedResponseOffset<Book>(entities, pageNumber, pageSize, totalRecords);

        return pagedResponse;
    }

    public Task RemoveBookAsync(Book book)
    {
        _dbContext.Books.Remove(book);

        return Task.CompletedTask;
    }

    public Task UpdateBookAsync(Book book)
    {
        _dbContext.Books.Update(book);

        return Task.CompletedTask;
    }
}
