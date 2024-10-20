namespace LibraryTJRJ.Domain.Authors;

public interface IAuthorRepository
{
    Task AddAsync(Author author);
    Task<Author?> GetByIdAsync(Guid id);
    Task<List<Author>> GetByIdsAsync(List<Guid> authorIds);
    Task<List<Author>> GetAllAsync();
    Task<bool> ExistsAsync(Guid id);
    Task UpdateAuthorAsync(Author author);
    Task RemoveAuthorAsync(Author author);
    Task<List<Author>> GetAllByBookAsync(Guid bookId);
}
