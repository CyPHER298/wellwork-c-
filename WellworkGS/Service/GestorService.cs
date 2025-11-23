using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class GestorService
{
    private readonly AppDbContext _context;

    public GestorService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<GestorReadDTO>> GetAllAsync()
    {
        return await _context.Gestores
            .Select(g => new GestorReadDTO
            {
                IdGestor = g.IdGestor,
                NomeGestor = g.NomeGestor,
                EmailGestor = g.EmailGestor,
                CargoGestor = g.CargoGestor,
                Departamento = g.Departamento
            })
            .ToListAsync();
    }

    public async Task<GestorReadDTO?> GetByIdAsync(int id)
    {
        var gestor = await _context.Gestores.FindAsync(id);
        return gestor == null
            ? null
            : new GestorReadDTO
            {
                IdGestor = gestor.IdGestor,
                NomeGestor = gestor.NomeGestor,
                EmailGestor = gestor.EmailGestor,
                CargoGestor = gestor.CargoGestor,
                Departamento = gestor.Departamento
            };
    }

    public async Task<GestorReadDTO> CreateAsync(GestorCreateDTO dto)
    {
        var entity = new Gestor
        {
            NomeGestor = dto.NomeGestor,
            EmailGestor = dto.EmailGestor,
            SenhaGestor = dto.SenhaGestor,
            CargoGestor = dto.CargoGestor,
            Departamento = dto.Departamento
        };

        _context.Gestores.Add(entity);
        await _context.SaveChangesAsync();

        return new GestorReadDTO
        {
            IdGestor = entity.IdGestor,
            NomeGestor = entity.NomeGestor,
            EmailGestor = entity.EmailGestor,
            CargoGestor = entity.CargoGestor,
            Departamento = entity.Departamento
        };
    }

    public async Task<bool> UpdateAsync(int id, GestorUpdateDTO dto)
    {
        var gestor = await _context.Gestores.FindAsync(id);
        if (gestor == null) return false;

        gestor.NomeGestor = dto.NomeGestor;
        gestor.EmailGestor = dto.EmailGestor;
        gestor.CargoGestor = dto.CargoGestor;
        gestor.Departamento = dto.Departamento;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var gestor = await _context.Gestores.FindAsync(id);
        if (gestor == null) return false;

        _context.Gestores.Remove(gestor);
        await _context.SaveChangesAsync();
        return true;
    }
}