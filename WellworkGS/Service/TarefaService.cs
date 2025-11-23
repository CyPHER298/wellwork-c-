using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class TarefaService
{
private readonly ITarefaRepository _repository;

    public TarefaService(ITarefaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TarefaReadDTO>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();

        return list.Select(t => new TarefaReadDTO
        {
            IdTarefa = t.IdTarefa,
            IdUsuario = t.IdUsuario,
            TituloTarefa = t.TituloTarefa,
            DescricaoTarefa = t.DescricaoTarefa,
            DataHoraTarefa = t.DataHoraTarefa,
            StatusTarefa = t.StatusTarefa
        });
    }

    public async Task<TarefaReadDTO?> GetByIdAsync(int id)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return null;

        return new TarefaReadDTO
        {
            IdTarefa = t.IdTarefa,
            IdUsuario = t.IdUsuario,
            TituloTarefa = t.TituloTarefa,
            DescricaoTarefa = t.DescricaoTarefa,
            DataHoraTarefa = t.DataHoraTarefa,
            StatusTarefa = t.StatusTarefa
        };
    }

    public async Task<TarefaReadDTO> CreateAsync(TarefaCreateDTO dto)
    {
        var entity = new Tarefa
        {
            IdUsuario = dto.IdUsuario,
            TituloTarefa = dto.TituloTarefa,
            DescricaoTarefa = dto.DescricaoTarefa,
            DataHoraTarefa = dto.DataHoraTarefa,
            StatusTarefa = dto.StatusTarefa
        };

        await _repository.AddAsync(entity);

        return new TarefaReadDTO
        {
            IdTarefa = entity.IdTarefa,
            IdUsuario = entity.IdUsuario,
            TituloTarefa = entity.TituloTarefa,
            DescricaoTarefa = entity.DescricaoTarefa,
            DataHoraTarefa = entity.DataHoraTarefa,
            StatusTarefa = entity.StatusTarefa
        };
    }

    public async Task<bool> UpdateAsync(int id, TarefaUpdateDTO dto)
    {
        var t = await _repository.GetByIdAsync(id);
        if (t == null) return false;

        t.TituloTarefa = dto.TituloTarefa;
        t.DescricaoTarefa = dto.DescricaoTarefa;
        t.DataHoraTarefa = dto.DataHoraTarefa;
        t.StatusTarefa = dto.StatusTarefa;

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