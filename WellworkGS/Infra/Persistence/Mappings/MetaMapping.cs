using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Mappings;

public class MetaMapping : IEntityTypeConfiguration<Meta>
{
    public void Configure(EntityTypeBuilder<Meta> builder)
    {
        builder.ToTable("META");

        builder.HasKey(m => m.IdMeta);

        builder.Property(m => m.IdMeta)
            .HasColumnName("ID")
            .ValueGeneratedOnAdd();

        builder.Property(m => m.TituloMeta)
            .HasColumnName("TITULO")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(m => m.DescricaoMeta)
            .HasColumnName("DESCRICAO")
            .HasMaxLength(90);

        builder.Property(m => m.IdUsuario)
            .HasColumnName("USUARIO_ID");

        builder.HasOne(m => m.Usuario)
            .WithMany(u => u.Metas)
            .HasForeignKey(m => m.IdUsuario);
    }
}