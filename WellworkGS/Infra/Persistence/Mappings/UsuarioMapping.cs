using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class UsuarioMapping : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("USUARIO");

        builder.HasKey(u => u.IdUsuario);

        builder.Property(u => u.IdUsuario)
            .HasColumnName("IDUSUARIO")
            .ValueGeneratedOnAdd();

        builder.Property(u => u.NomeUsuario)
            .HasColumnName("NOME_USUARIO")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(u => u.EmailUsuario)
            .HasColumnName("EMAIL_USUARIO")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(u => u.SenhaUsuario)
            .HasColumnName("SENHA_USUARIO")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(u => u.CargoUsuario)
            .HasColumnName("CARGO_USUARIO")
            .HasMaxLength(30);

        builder.Property(u => u.Acessibilidade)
            .HasColumnName("ACESSIBILIDADE")
            .HasMaxLength(50);

        builder.HasIndex(u => u.EmailUsuario)
            .IsUnique();

    }
}