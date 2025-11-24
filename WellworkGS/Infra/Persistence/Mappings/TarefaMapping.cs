using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class TarefaMapping : IEntityTypeConfiguration<Tarefa>
{
    public void Configure(EntityTypeBuilder<Tarefa> builder)
    {
        builder.ToTable("TAREFA");

        builder.HasKey(t => t.IdTarefa);

        builder.Property(t => t.IdTarefa)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();

        builder.Property(t => t.IdUsuario)
            .HasColumnName("USUARIO_ID")
            .IsRequired();

        builder.Property(t => t.TituloTarefa)
            .HasColumnName("TITULO")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(t => t.DescricaoTarefa)
            .HasColumnName("DESCRICAO")
            .HasMaxLength(100);

        builder.Property(t => t.DataHoraTarefa)
            .HasColumnName("DATAHORA");

        builder.Property(t => t.StatusTarefa)
            .HasColumnName("STATUS")
            .HasMaxLength(20);

        builder.HasOne(t => t.Usuario)
            .WithMany(u => u.Tarefas)
            .HasForeignKey(t => t.IdUsuario);
    }
}
