using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using LibraryTJRJ.Domain.User;

namespace LibraryTJRJ.Infrastructure.Users.Persistence;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        // Configuração da tabela
        builder.ToTable("Users");

        // Chave primária
        builder.HasKey(u => u.Id);

        builder.Property(s => s.Id)
            .ValueGeneratedNever();

        // Propriedades obrigatórias
        builder.Property(u => u.FirstName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.LastName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);

        // Datas
        builder.Property(u => u.CreatedDateTime);

        builder.Property(u => u.UpdatedDateTime);

        // Índice único para Email
        builder.HasIndex(u => u.Email)
            .IsUnique();
    }
}
