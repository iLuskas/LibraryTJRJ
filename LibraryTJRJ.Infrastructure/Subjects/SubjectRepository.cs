using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Infrastructure.Common.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Subjects
{
    public class SubjectRepository(LibraryTJRJDbContext dbContext) : ISubjectRepository
    {
        private readonly LibraryTJRJDbContext _dbContext = dbContext;

        public async Task AddAsync(Subject subject)
        {
            await _dbContext.Subjects.AddAsync(subject);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _dbContext.Subjects.AsNoTracking().AnyAsync(subject => subject.Id == id);
        }

        public async Task<List<Subject>> GetAllAsync()
        {
            return await _dbContext.Subjects.ToListAsync();
        }

        public async Task<List<Subject>> GetAllByBookAsync(Guid bookId)
        {
            return await _dbContext.Subjects
                .Where(subject => subject.Books.Any(book => book.Id == bookId))
                .Include(subject => subject.Books)
                .ToListAsync();
        }

        public async Task<Subject?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Subjects.FirstOrDefaultAsync(subject => subject.Id == id);
        }

        public Task RemoveSubjectAsync(Subject subject)
        {
            _dbContext.Subjects.Remove(subject);

            return Task.CompletedTask;
        }

        public Task UpdateSubjectAsync(Subject subject)
        {
            _dbContext.Subjects.Update(subject);

            return Task.CompletedTask;
        }
    }
}
