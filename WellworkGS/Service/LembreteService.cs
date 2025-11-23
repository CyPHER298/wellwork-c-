using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class LembreteService
{
     private readonly ILembreteRepository _repository;

    public LembreteService(ILembreteRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<LembreteReadDTO>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();

        return list.Select(l => new LembreteReadDTO
        {
            IdLembrete = l.IdLembrete,
            IdUsuario = l.IdUsuario,
            TipoLembrete = l.TipoLembrete,
            Frequencia = l.Frequencia,
            Ativo = l.Ativo
        });
    }

    public async Task<LembreteReadDTO?> GetByIdAsync(int id)
    {
        var l = await _repository.GetByIdAsync(id);
        if (l == null) return null;

        return new LembreteReadDTO
        {
            IdLembrete = l.IdLembrete,
            IdUsuario = l.IdUsuario,
            TipoLembrete = l.TipoLembrete,
            Frequencia = l.Frequencia,
            Ativo = l.Ativo
        };
    }

    public async Task<LembreteReadDTO> CreateAsync(LembreteCreateDTO dto)
    {
        var entity = new Lembrete
        {
            IdUsuario = dto.IdUsuario,
            TipoLembrete = dto.TipoLembrete,
            Frequencia = dto.Frequencia,
            Ativo = dto.Ativo
        };

        await _repository.AddAsync(entity);

        return new LembreteReadDTO
        {
            IdLembrete = entity.IdLembrete,
            IdUsuario = entity.IdUsuario,
            TipoLembrete = entity.TipoLembrete,
            Frequencia = entity.Frequencia,
            Ativo = entity.Ativo
        };
    }

    public async Task<bool> UpdateAsync(int id, LembreteUpdateDTO dto)
    {
        var l = await _repository.GetByIdAsync(id);
        if (l == null) return false;

        l.TipoLembrete = dto.TipoLembrete;
        l.Frequencia = dto.Frequencia;
        l.Ativo = dto.Ativo;

        _repository.Update(l);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var l = await _repository.GetByIdAsync(id);
        if (l == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}