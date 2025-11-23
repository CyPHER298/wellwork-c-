using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class MetaService
{
    private readonly AppDbContext _context;

        public MetaService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<MetaReadDTO>> GetAllAsync()
        {
            return await _context.Metas
                .Select(m => new MetaReadDTO
                {
                    IdMeta = m.IdMeta,
                    TituloMeta = m.TituloMeta,
                    DescricaoMeta = m.DescricaoMeta,
                    IdUsuario = m.IdUsuario
                }).ToListAsync();
        }

        public async Task<MetaReadDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Metas.FindAsync(id);
            return entity == null
                ? null
                : new MetaReadDTO
                {
                    IdMeta = entity.IdMeta,
                    TituloMeta = entity.TituloMeta,
                    DescricaoMeta = entity.DescricaoMeta,
                    IdUsuario = entity.IdUsuario
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

            _context.Metas.Add(entity);
            await _context.SaveChangesAsync();

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
            var entity = await _context.Metas.FindAsync(id);
            if (entity == null) return false;

            entity.TituloMeta = dto.TituloMeta;
            entity.DescricaoMeta = dto.DescricaoMeta;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Metas.FindAsync(id);
            if (entity == null) return false;

            _context.Metas.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
}