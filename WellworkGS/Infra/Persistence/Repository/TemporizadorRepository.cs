using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class TemporizadorRepository : ITemporizadorRepository
{
    private readonly AppDbContext _context;

    public TemporizadorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Temporizador>> GetAllAsync()
    {
        return await _context.Temporizadores.ToListAsync();
    }

    public async Task<Temporizador?> GetByIdAsync(int id)
    {
        return await _context.Temporizadores.FindAsync(id);
    }

    public async Task AddAsync(Temporizador temporizador)
    {
        await _context.Temporizadores.AddAsync(temporizador);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var temporizador = await _context.Temporizadores.FindAsync(id);
        if (temporizador != null)
            _context.Temporizadores.Remove(temporizador);

        await _context.SaveChangesAsync();
    }

    public void Update(Temporizador temporizador)
    {
        _context.Temporizadores.Update(temporizador);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}