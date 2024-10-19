using ErrorOr;

namespace LibraryTJRJ.Domain.Books;

public interface IBookRepository
{
    Task AddAsync(Book book);
    Task<Book?> GetByIdAsync(Guid id);
    Task<List<Book>> GetAllAsync();
    Task<List<Book>> GetAllByAuthorAsync(Guid authorId);
    Task<bool> ExistsAsync(Guid id);
    Task UpdateBookAsync(Book book);
    Task RemoveBookAsync(Book book);
}
