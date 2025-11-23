using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class MetaService
{
    private readonly IMetaRepository _repository;

    public MetaService(IMetaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MetaReadDTO>> GetAllAsync()
    {
        var list = await _repository.GetAllAsync();

        return list.Select(m => new MetaReadDTO
        {
            IdMeta = m.IdMeta,
            TituloMeta = m.TituloMeta,
            DescricaoMeta = m.DescricaoMeta,
            IdUsuario = m.IdUsuario
        });
    }

    public async Task<MetaReadDTO?> GetByIdAsync(int id)
    {
        var m = await _repository.GetByIdAsync(id);
        if (m == null) return null;

        return new MetaReadDTO
        {
            IdMeta = m.IdMeta,
            TituloMeta = m.TituloMeta,
            DescricaoMeta = m.DescricaoMeta,
            IdUsuario = m.IdUsuario
        };
    }

    public async Task<MetaReadDTO> CreateAsync(MetaCreateDTO dto)
    {
        var entity = new Meta
        {
            TituloMeta = dto.TituloMeta,
            DescricaoMeta = dto.DescricaoMeta,
            IdUsuario = dto.IdUsuario
        };

        await _repository.AddAsync(entity);

        return new MetaReadDTO
        {
            IdMeta = entity.IdMeta,
            TituloMeta = entity.TituloMeta,
            DescricaoMeta = entity.DescricaoMeta,
            IdUsuario = entity.IdUsuario
        };
    }

    public async Task<bool> UpdateAsync(int id, MetaUpdateDTO dto)
    {
        var m = await _repository.GetByIdAsync(id);
        if (m == null) return false;

        m.TituloMeta = dto.TituloMeta;
        m.DescricaoMeta = dto.DescricaoMeta;

        _repository.Update(m);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var m = await _repository.GetByIdAsync(id);
        if (m == null) return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}