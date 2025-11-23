using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface ILembreteRepository
{
    Task<List<Lembrete>> GetAllAsync();
    Task<Lembrete?> GetByIdAsync(int id);
    Task AddAsync(Lembrete lembrete);
    Task DeleteAsync(int id);
    void Update(Lembrete lembrete);
    Task<int> SaveChangesAsync();
}