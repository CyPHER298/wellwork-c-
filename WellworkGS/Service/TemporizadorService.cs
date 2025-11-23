using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class TemporizadorService
{
    private readonly ITemporizadorRepository _repository;

    public TemporizadorService(ITemporizadorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TemporizadorReadDTO>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();

        return list.Select(t => new TemporizadorReadDTO
        {
            IdTemporizador = t.IdTemporizador,
            IdUsuario = t.IdUsuario,
            TipoTemporizador = t.TipoTemporizador,
            Duracao = t.Duracao,
            Inicio = t.Inicio,
            Fim = t.Fim,
            StatusTemporizador = t.StatusTemporizador
        });
    }

    public async Task<TemporizadorReadDTO?> GetByIdAsync(int id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return null;

        return new TemporizadorReadDTO
        {
            IdTemporizador = t.IdTemporizador,
            IdUsuario = t.IdUsuario,
            TipoTemporizador = t.TipoTemporizador,
            Duracao = t.Duracao,
            Inicio = t.Inicio,
            Fim = t.Fim,
            StatusTemporizador = t.StatusTemporizador
        };
    }

    public async Task<TemporizadorReadDTO> CreateAsync(TemporizadorCreateDTO dto)
    {
        var entity = new Temporizador
        {
            IdUsuario = dto.IdUsuario,
            TipoTemporizador = dto.TipoTemporizador,
            Duracao = dto.Duracao,
            Inicio = dto.Inicio,
            Fim = dto.Fim,
            StatusTemporizador = dto.StatusTemporizador
        };

        await _repository.AddAsync(entity);

        return new TemporizadorReadDTO
        {
            IdTemporizador = entity.IdTemporizador,
            IdUsuario = entity.IdUsuario,
            TipoTemporizador = entity.TipoTemporizador,
            Duracao = entity.Duracao,
            Inicio = entity.Inicio,
            Fim = entity.Fim,
            StatusTemporizador = entity.StatusTemporizador
        };
    }

    public async Task<bool> UpdateAsync(int id, TemporizadorUpdateDTO dto)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return false;

        t.TipoTemporizador = dto.TipoTemporizador;
        t.Duracao = dto.Duracao;
        t.Inicio = dto.Inicio;
        t.Fim = dto.Fim;
        t.StatusTemporizador = dto.StatusTemporizador;

        _repository.Update(t);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}