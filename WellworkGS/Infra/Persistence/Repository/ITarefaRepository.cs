using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface ITarefaRepository
{
    Task<List<Tarefa>> GetAllAsync();
    Task<Tarefa?> GetByIdAsync(int id);
    Task AddAsync(Tarefa tarefa);
    Task DeleteAsync(int id);
    void Update(Tarefa tarefa);
    Task<int> SaveChangesAsync();
}