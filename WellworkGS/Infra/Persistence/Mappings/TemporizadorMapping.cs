using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class TemporizadorMapping : IEntityTypeConfiguration<Temporizador>
{
    public void Configure(EntityTypeBuilder<Temporizador> builder)
    {
        builder.ToTable("TIMER");

        builder.HasKey(t => t.IdTemporizador);

        builder.Property(t => t.IdTemporizador)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.IdUsuario)
            .HasColumnName("USUARIO_ID")
            .IsRequired();

        builder.Property(t => t.TipoTemporizador)
            .HasColumnName("TIPO")
            .HasMaxLength(20);

        builder.Property(t => t.Duracao)
            .HasColumnName("DURACAO");

        builder.Property(t => t.Inicio)
            .HasColumnName("INICIO");

        builder.Property(t => t.Fim)
            .HasColumnName("FIM");

        builder.Property(t => t.StatusTemporizador)
            .HasColumnName("STATUS")
            .HasMaxLength(20);

        builder.HasOne(t => t.Usuario)
            .WithMany(u => u.Temporizadores)
            .HasForeignKey(t => t.IdUsuario);
    }
}