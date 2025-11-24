using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("Usuario");

        builder.HasKey(u => u.IdUsuario);

        builder.Property(u => u.IdUsuario)
            .HasColumnName("idUsuario")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.NomeUsuario)
            .HasColumnName("nome_usuario")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(u => u.EmailUsuario)
            .HasColumnName("email_usuario")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(u => u.SenhaUsuario)
            .HasColumnName("senha_usuario")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(u => u.CargoUsuario)
            .HasColumnName("cargo_usuario")
            .HasMaxLength(30);

        builder.Property(u => u.Acessibilidade)
            .HasColumnName("acessibilidade")
            .HasMaxLength(50);

        builder.HasIndex(u => u.EmailUsuario)
            .IsUnique();

    }
}