using Microsoft.EntityFrameworkCore;
using WellworkGS.Infra.Persistence.Mappings;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSets
    public DbSet<Usuario> Usuarios => Set<Usuario>();
    public DbSet<Gestor> Gestores => Set<Gestor>();
    public DbSet<Tarefa> Tarefas => Set<Tarefa>();
    public DbSet<Temporizador> Temporizadores => Set<Temporizador>();
    public DbSet<Meta> Metas => Set<Meta>();
    public DbSet<Lembrete> Lembretes => Set<Lembrete>();
    public DbSet<AlertaCrise> AlertasCrise => Set<AlertaCrise>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UsuarioMapping());
        modelBuilder.ApplyConfiguration(new GestorMapping());
        modelBuilder.ApplyConfiguration(new TarefaMapping());
        modelBuilder.ApplyConfiguration(new TemporizadorMapping());
        modelBuilder.ApplyConfiguration(new MetaMapping());
        modelBuilder.ApplyConfiguration(new LembreteMapping());
        modelBuilder.ApplyConfiguration(new AlertaCriseMapping());

        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Usuario)
            .WithMany(u => u.Tarefas)
            .HasForeignKey(t => t.IdUsuario);

        modelBuilder.Entity<Temporizador>()
            .HasOne(t => t.Usuario)
            .WithMany(u => u.Temporizadores)
            .HasForeignKey(t => t.IdUsuario);

        modelBuilder.Entity<Meta>()
            .HasOne(m => m.Usuario)
            .WithMany(u => u.Metas)
            .HasForeignKey(m => m.IdUsuario);

        modelBuilder.Entity<Lembrete>()
            .HasOne(l => l.Usuario)
            .WithMany(u => u.Lembretes)
            .HasForeignKey(l => l.IdUsuario);

        modelBuilder.Entity<AlertaCrise>()
            .HasOne(a => a.Usuario)
            .WithMany(u => u.AlertasDeCrise)
            .HasForeignKey(a => a.IdUsuario);

        modelBuilder.Entity<AlertaCrise>()
            .HasOne(a => a.Gestor)
            .WithMany(g => g.AlertasDeCrise)
            .HasForeignKey(a => a.IdGestor);

        base.OnModelCreating(modelBuilder);
    }
}
