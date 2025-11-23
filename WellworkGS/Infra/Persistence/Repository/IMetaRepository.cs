using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface IMetaRepository
{
    Task<List<Meta>> GetAllAsync();
    Task<Meta?> GetByIdAsync(int id);
    Task AddAsync(Meta meta);
    Task DeleteAsync(int id);
    void Update(Meta meta);
    Task<int> SaveChangesAsync();
}