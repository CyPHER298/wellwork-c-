using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class AlertaCriseRepository : IAlertaCriseRepository
{
    private readonly AppDbContext _context;

    public AlertaCriseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<AlertaCrise>> GetAllAsync()
    {
        return await _context.AlertasCrise.ToListAsync();
    }

    public async Task<AlertaCrise?> GetByIdAsync(int id)
    {
        return await _context.AlertasCrise.FindAsync(id);
    }

    public async Task AddAsync(AlertaCrise alerta)
    {
        await _context.AlertasCrise.AddAsync(alerta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var alerta = await _context.AlertasCrise.FindAsync(id);
        if (alerta != null)
            _context.AlertasCrise.Remove(alerta);

        await _context.SaveChangesAsync();
    }

    public void Update(AlertaCrise alerta)
    {
        _context.AlertasCrise.Update(alerta);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}