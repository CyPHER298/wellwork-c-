using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class LembreteMapping : IEntityTypeConfiguration<Lembrete>
{
    public void Configure(EntityTypeBuilder<Lembrete> builder)
    {
        builder.ToTable("LEMBRETE");

        builder.HasKey(l => l.IdLembrete);

        builder.Property(l => l.IdLembrete)
            .HasColumnName("IDLEMBRETE")
            .ValueGeneratedOnAdd();

        builder.Property(l => l.IdUsuario)
            .HasColumnName("IDUSUARIO")
            .IsRequired();

        builder.Property(l => l.TipoLembrete)
            .HasColumnName("TIPO_LEMBRETE")
            .HasMaxLength(50);

        builder.Property(l => l.Frequencia)
            .HasColumnName("FREQUENCIA");

        builder.Property(l => l.Ativo)
            .HasColumnName("ATIVO")
            .HasMaxLength(1);

        builder.HasOne(l => l.Usuario)
            .WithMany(u => u.Lembretes)
            .HasForeignKey(l => l.IdUsuario);
    }
}