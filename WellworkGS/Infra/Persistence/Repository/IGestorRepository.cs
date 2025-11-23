using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface IGestorRepository
{
    Task<List<Gestor>> GetAllAsync();
    Task<Gestor?> GetByIdAsync(int id);
    Task AddAsync(Gestor gestor);
    Task DeleteAsync(int id);
    void Update(Gestor gestor);
    Task<int> SaveChangesAsync();
}