using LibraryTJRJ.Domain.Subjects;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Subjects;

public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        // Configuração da tabela
        builder.ToTable("Subjects");

        // Chave primária
        builder.HasKey(s => s.Id);

        // Propriedades
        builder.Property(s => s.Description)
            .HasMaxLength(20);

        // Datas
        builder.Property(s => s.CreatedDateTime);

        builder.Property(s => s.UpdatedDateTime);

        // Relacionamento de um para muitos com livros
        builder.HasMany(s => s.Books)
               .WithMany(b => b.Subjects)
               .UsingEntity(j => j.ToTable("BookSubjects")); // Nome da tabela de junção
    }
}
