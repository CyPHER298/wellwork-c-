using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class MetaRepository : IMetaRepository
{
    private readonly AppDbContext _context;

    public MetaRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Meta>> GetAllAsync()
    {
        return await _context.Metas.ToListAsync();
    }

    public async Task<Meta?> GetByIdAsync(int id)
    {
        return await _context.Metas.FindAsync(id);
    }

    public async Task AddAsync(Meta meta)
    {
        await _context.Metas.AddAsync(meta);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var meta = await _context.Metas.FindAsync(id);
        if (meta != null)
            _context.Metas.Remove(meta);

        await _context.SaveChangesAsync();
    }

    public void Update(Meta meta)
    {
        _context.Metas.Update(meta);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}
