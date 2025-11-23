using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class TarefaService
{
    private readonly AppDbContext _context;

    public TarefaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TarefaReadDTO>> GetAllAsync()
    {
        return await _context.Tarefas
            .Select(t => new TarefaReadDTO
            {
                IdTarefa = t.IdTarefa,
                IdUsuario = t.IdUsuario,
                TituloTarefa = t.TituloTarefa,
                DescricaoTarefa = t.DescricaoTarefa,
                DataHoraTarefa = t.DataHoraTarefa,
                StatusTarefa = t.StatusTarefa
            }).ToListAsync();
    }

    public async Task<TarefaReadDTO?> GetByIdAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        return tarefa == null
            ? null
            : new TarefaReadDTO
            {
                IdTarefa = tarefa.IdTarefa,
                IdUsuario = tarefa.IdUsuario,
                TituloTarefa = tarefa.TituloTarefa,
                DescricaoTarefa = tarefa.DescricaoTarefa,
                DataHoraTarefa = tarefa.DataHoraTarefa,
                StatusTarefa = tarefa.StatusTarefa
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

        _context.Tarefas.Add(entity);
        await _context.SaveChangesAsync();

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
        var entity = await _context.Tarefas.FindAsync(id);
        if (entity == null) return false;

        entity.TituloTarefa = dto.TituloTarefa;
        entity.DescricaoTarefa = dto.DescricaoTarefa;
        entity.DataHoraTarefa = dto.DataHoraTarefa;
        entity.StatusTarefa = dto.StatusTarefa;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var entity = await _context.Tarefas.FindAsync(id);
        if (entity == null) return false;

        _context.Tarefas.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}