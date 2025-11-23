using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class TarefaRepository : ITarefaRepository
{
    private readonly AppDbContext _context;

    public TarefaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Tarefa>> GetAllAsync()
    {
        return await _context.Tarefas.ToListAsync();
    }

    public async Task<Tarefa?> GetByIdAsync(int id)
    {
        return await _context.Tarefas.FindAsync(id);
    }

    public async Task AddAsync(Tarefa tarefa)
    {
        await _context.Tarefas.AddAsync(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa != null)
            _context.Tarefas.Remove(tarefa);

        await _context.SaveChangesAsync();
    }

    public void Update(Tarefa tarefa)
    {
        _context.Tarefas.Update(tarefa);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}