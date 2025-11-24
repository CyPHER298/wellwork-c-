using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class AlertaCriseMapping : IEntityTypeConfiguration<AlertaCrise>
{
    public void Configure(EntityTypeBuilder<AlertaCrise> builder)
    {
        builder.ToTable("ALERTA_CRISE");

        builder.HasKey(a => a.IdAlertaCrise);

        builder.Property(a => a.IdAlertaCrise)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.IdUsuario)
            .HasColumnName("USUARIO_ID")
            .IsRequired();

        builder.Property(a => a.IdGestor)
            .HasColumnName("GESTOR_ID")
            .IsRequired();

        builder.Property(a => a.DataHoraAlerta)
            .HasColumnName("DATAHORA");

        builder.Property(a => a.StatusAlerta)
            .HasColumnName("STATUS")
            .HasMaxLength(20);

        builder.HasOne(a => a.Usuario)
            .WithMany(u => u.AlertasDeCrise)
            .HasForeignKey(a => a.IdUsuario);

        builder.HasOne(a => a.Gestor)
            .WithMany(g => g.AlertasDeCrise)
            .HasForeignKey(a => a.IdGestor);
    }
}