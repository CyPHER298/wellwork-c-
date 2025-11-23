using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class GestorRepository : IGestorRepository
{
    private readonly AppDbContext _context;

    public GestorRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Gestor>> GetAllAsync()
    {
        return await _context.Gestores.ToListAsync();
    }

    public async Task<Gestor?> GetByIdAsync(int id)
    {
        return await _context.Gestores.FindAsync(id);
    }

    public async Task AddAsync(Gestor gestor)
    {
        await _context.Gestores.AddAsync(gestor);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var gestor = await _context.Gestores.FindAsync(id);
        if (gestor != null)
            _context.Gestores.Remove(gestor);

        await _context.SaveChangesAsync();
    }

    public void Update(Gestor gestor)
    {
        _context.Gestores.Update(gestor);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}