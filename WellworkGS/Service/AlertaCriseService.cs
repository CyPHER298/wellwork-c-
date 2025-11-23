using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class AlertaCriseService
{
private readonly IAlertaCriseRepository _repository;

    public AlertaCriseService(IAlertaCriseRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<AlertaCriseReadDTO>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();

        return list.Select(a => new AlertaCriseReadDTO
        {
            IdAlertaCrise = a.IdAlertaCrise,
            IdUsuario = a.IdUsuario,
            IdGestor = a.IdGestor,
            DataHoraAlerta = a.DataHoraAlerta,
            StatusAlerta = a.StatusAlerta
        });
    }

    public async Task<AlertaCriseReadDTO?> GetByIdAsync(int id)
    {
        var a = await _repository.GetByIdAsync(id);
        if (a == null) return null;

        return new AlertaCriseReadDTO
        {
            IdAlertaCrise = a.IdAlertaCrise,
            IdUsuario = a.IdUsuario,
            IdGestor = a.IdGestor,
            DataHoraAlerta = a.DataHoraAlerta,
            StatusAlerta = a.StatusAlerta
        };
    }

    public async Task<AlertaCriseReadDTO> CreateAsync(AlertaCriseCreateDTO dto)
    {
        var entity = new AlertaCrise
        {
            IdUsuario = dto.IdUsuario,
            IdGestor = dto.IdGestor,
            StatusAlerta = dto.StatusAlerta,
            DataHoraAlerta = DateTime.Now
        };

        await _repository.AddAsync(entity);

        return new AlertaCriseReadDTO
        {
            IdAlertaCrise = entity.IdAlertaCrise,
            IdUsuario = entity.IdUsuario,
            IdGestor = entity.IdGestor,
            DataHoraAlerta = entity.DataHoraAlerta,
            StatusAlerta = entity.StatusAlerta
        };
    }

    public async Task<bool> UpdateAsync(int id, AlertaCriseUpdateDTO dto)
    {
        var a = await _repository.GetByIdAsync(id);
        if (a == null) return false;

        a.StatusAlerta = dto.StatusAlerta;

        _repository.Update(a);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var a = await _repository.GetByIdAsync(id);
        if (a == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}