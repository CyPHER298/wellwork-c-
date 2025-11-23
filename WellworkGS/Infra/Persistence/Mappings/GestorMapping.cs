using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class GestorMapping : IEntityTypeConfiguration<Gestor>
{
    public void Configure(EntityTypeBuilder<Gestor> builder)
    {
        builder.ToTable("GESTOR");

        builder.HasKey(g => g.IdGestor);

        builder.Property(g => g.IdGestor)
            .HasColumnName("IDGESTOR")
            .ValueGeneratedOnAdd();

        builder.Property(g => g.NomeGestor)
            .HasColumnName("NOME_GESTOR")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(g => g.EmailGestor)
            .HasColumnName("EMAIL_GESTOR")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(g => g.SenhaGestor)
            .HasColumnName("SENHA_GESTOR")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(g => g.CargoGestor)
            .HasColumnName("CARGO_GESTOR")
            .HasMaxLength(30);

        builder.Property(g => g.Departamento)
            .HasColumnName("DEPARTAMENTO")
            .HasMaxLength(100);

        builder.HasIndex(g => g.EmailGestor)
            .IsUnique();
    }
}