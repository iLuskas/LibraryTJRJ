using LibraryTJRJ.Domain.Books;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Books.Persistence;

public class BookConfiguration : IEntityTypeConfiguration<Book>
{
    public void Configure(EntityTypeBuilder<Book> builder)
    {
        // Configuração da tabela
        builder.ToTable("Books");

        // Chave primária
        builder.HasKey(b => b.Id);

        // Propriedades
        builder.Property(b => b.Title)
            .HasMaxLength(40); 

        builder.Property(b => b.Publisher)
            .HasMaxLength(40); 

        builder.Property(b => b.Edition);

        builder.Property(b => b.YearPublication)
            .HasMaxLength(4);

        builder.Property(b => b.CreatedDateTime);

        builder.Property(b => b.UpdatedDateTime);

        // Relacionamento com autores (Many-to-Many)
        builder.HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("AuthorBooks")); // Tabela de junção

        // Relacionamento com assuntos (Many-to-Many)
        builder.HasMany(b => b.Subjects)
            .WithMany(s => s.Books)
            .UsingEntity(j => j.ToTable("BookSubjects")); // Tabela de junção
    }
}
