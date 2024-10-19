using LibraryTJRJ.Application.Common.Exceptions;
using LibraryTJRJ.Domain.Authors;
using LibraryTJRJ.Domain.Books;
using LibraryTJRJ.Domain.Common.Interfaces;
using LibraryTJRJ.Domain.Subjects;
using LibraryTJRJ.Domain.User;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection;

namespace LibraryTJRJ.Infrastructure.Common.Persistence;

public class LibraryTJRJDbContext(DbContextOptions options) : DbContext(options), IUnitOfWork
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Author> Authors { get; set; } = null!;
    public DbSet<Book> Books { get; set; } = null!;
    public DbSet<Subject> Subjects { get; set; } = null!;

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            int result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }
        catch (DbUpdateConcurrencyException ex)
        {
            throw new ConcurrencyException("Concurrency exception occurred.", ex);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }
}
