using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class GestorService
{
private readonly IGestorRepository _repository;

    public GestorService(IGestorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<GestorReadDTO>> GetAllAsync()
    {
        var items = await _repository.GetAllAsync();

        return items.Select(g => new GestorReadDTO
        {
            IdGestor = g.IdGestor,
            NomeGestor = g.NomeGestor,
            EmailGestor = g.EmailGestor,
            CargoGestor = g.CargoGestor,
            Departamento = g.Departamento
        });
    }

    public async Task<GestorReadDTO?> GetByIdAsync(int id)
    {
        var g = await _repository.GetByIdAsync(id);
        if (g == null) return null;

        return new GestorReadDTO
        {
            IdGestor = g.IdGestor,
            NomeGestor = g.NomeGestor,
            EmailGestor = g.EmailGestor,
            CargoGestor = g.CargoGestor,
            Departamento = g.Departamento
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

        await _repository.AddAsync(entity);

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
        var g = await _repository.GetByIdAsync(id);
        if (g == null) return false;

        g.NomeGestor = dto.NomeGestor;
        g.EmailGestor = dto.EmailGestor;
        g.CargoGestor = dto.CargoGestor;
        g.Departamento = dto.Departamento;

        _repository.Update(g);
        await _repository.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var g = await _repository.GetByIdAsync(id);
        if (g == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}