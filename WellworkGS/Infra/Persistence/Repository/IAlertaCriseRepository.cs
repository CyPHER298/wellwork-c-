using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface IAlertaCriseRepository
{
    Task<List<AlertaCrise>> GetAllAsync();
    Task<AlertaCrise?> GetByIdAsync(int id);
    Task AddAsync(AlertaCrise alerta);
    Task DeleteAsync(int id);
    void Update(AlertaCrise alerta);
    Task<int> SaveChangesAsync();
}