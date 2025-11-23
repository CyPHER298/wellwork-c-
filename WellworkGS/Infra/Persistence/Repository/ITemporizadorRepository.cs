using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface ITemporizadorRepository
{
    Task<List<Temporizador>> GetAllAsync();
    Task<Temporizador?> GetByIdAsync(int id);
    Task AddAsync(Temporizador temporizador);
    Task DeleteAsync(int id);
    void Update(Temporizador temporizador);
    Task<int> SaveChangesAsync();
}