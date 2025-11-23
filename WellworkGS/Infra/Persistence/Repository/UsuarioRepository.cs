using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly AppDbContext _context;

    public UsuarioRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<Usuario>> GetAllAsync()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetByIdAsync(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task AddAsync(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
            _context.Usuarios.Remove(usuario);

        await _context.SaveChangesAsync();
    }

    public void Update(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
    }

    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }
}