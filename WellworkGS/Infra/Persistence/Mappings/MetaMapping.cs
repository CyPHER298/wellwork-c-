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
            .HasColumnName("IDMETA")
            .ValueGeneratedOnAdd();

        builder.Property(m => m.TituloMeta)
            .HasColumnName("TITULO_META")
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(m => m.DescricaoMeta)
            .HasColumnName("DESCRICAO_META")
            .HasMaxLength(90);

        builder.Property(m => m.IdUsuario)
            .HasColumnName("IDUSUARIO");

        builder.HasOne(m => m.Usuario)
            .WithMany(u => u.Metas)
            .HasForeignKey(m => m.IdUsuario);
    }
}