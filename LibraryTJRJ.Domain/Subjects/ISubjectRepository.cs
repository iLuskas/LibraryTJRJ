namespace LibraryTJRJ.Domain.Subjects;

public interface ISubjectRepository
{
    Task AddAsync(Subject subject);
    Task<Subject?> GetByIdAsync(Guid id);
    Task<List<Subject>> GetByIdsAsync(List<Guid> subjectIds);
    Task<List<Subject>> GetAllAsync();
    Task<bool> ExistsAsync(Guid id);
    Task UpdateSubjectAsync(Subject subject);
    Task RemoveSubjectAsync(Subject subject);
    Task<List<Subject>> GetAllByBookAsync(Guid bookId);
}
