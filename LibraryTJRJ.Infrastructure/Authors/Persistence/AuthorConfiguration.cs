using LibraryTJRJ.Domain.Authors;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LibraryTJRJ.Infrastructure.Authors.Persistence;

public class AuthorConfiguration : IEntityTypeConfiguration<Author>
{
    public void Configure(EntityTypeBuilder<Author> builder)
    {
        // Configuração da tabela
        builder.ToTable("Authors");

        // Chave primária
        builder.HasKey(a => a.Id);
        
        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        // Propriedades obrigatórias
        builder.Property(a => a.Name)
            .HasMaxLength(40);

        // Datas
        builder.Property(a => a.CreatedDateTime);

        builder.Property(a => a.UpdatedDateTime);

        // Relacionamento de muitos para muitos com livros
        builder.HasMany(a => a.Books)
               .WithMany(b => b.Authors)
               .UsingEntity(j => j.ToTable("AuthorBooks")); // Nome da tabela de junção

        
    }
}
