using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class GestorMapping : IEntityTypeConfiguration<Gestor>
{
    public void Configure(EntityTypeBuilder<Gestor> builder)
    {
        builder.ToTable("Gestor");

        builder.HasKey(g => g.IdGestor);

        builder.Property(g => g.IdGestor)
            .HasColumnName("idGestor")
            .ValueGeneratedOnAdd();

        builder.Property(g => g.NomeGestor)
            .HasColumnName("nome_gestor")
            .HasMaxLength(60)
            .IsRequired();

        builder.Property(g => g.EmailGestor)
            .HasColumnName("email_gestor")
            .HasMaxLength(40)
            .IsRequired();

        builder.Property(g => g.SenhaGestor)
            .HasColumnName("senha_gestor")
            .HasMaxLength(10)
            .IsRequired();

        builder.Property(g => g.CargoGestor)
            .HasColumnName("cargo_gestor")
            .HasMaxLength(30);

        builder.Property(g => g.Departamento)
            .HasColumnName("departamento")
            .HasMaxLength(100);

        builder.HasIndex(g => g.EmailGestor)
            .IsUnique();
    }
}