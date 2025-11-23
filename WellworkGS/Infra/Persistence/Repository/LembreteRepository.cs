using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class LembreteRepository : ILembreteRepository
{
    private readonly AppDbContext _context;

    public LembreteRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Lembrete>> GetAllAsync()
    {
        return await _context.Lembretes.ToListAsync();
    }

    public async Task<Lembrete?> GetByIdAsync(int id)
    {
        return await _context.Lembretes.FindAsync(id);
    }

    public async Task AddAsync(Lembrete lembrete)
    {
        await _context.Lembretes.AddAsync(lembrete);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var lembrete = await _context.Lembretes.FindAsync(id);
        if (lembrete != null)
            _context.Lembretes.Remove(lembrete);

        await _context.SaveChangesAsync();
    }

    public void Update(Lembrete lembrete)
    {
        _context.Lembretes.Update(lembrete);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}