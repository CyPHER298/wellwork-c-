using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class LembreteService
{
    private readonly AppDbContext _context;

        public LembreteService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LembreteReadDTO>> GetAllAsync()
        {
            return await _context.Lembretes
                .Select(l => new LembreteReadDTO
                {
                    IdLembrete = l.IdLembrete,
                    IdUsuario = l.IdUsuario,
                    TipoLembrete = l.TipoLembrete,
                    Frequencia = l.Frequencia,
                    Ativo = l.Ativo
                }).ToListAsync();
        }

        public async Task<LembreteReadDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.Lembretes.FindAsync(id);
            return entity == null
                ? null
                : new LembreteReadDTO
                {
                    IdLembrete = entity.IdLembrete,
                    IdUsuario = entity.IdUsuario,
                    TipoLembrete = entity.TipoLembrete,
                    Frequencia = entity.Frequencia,
                    Ativo = entity.Ativo
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

            _context.Lembretes.Add(entity);
            await _context.SaveChangesAsync();

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
            var entity = await _context.Lembretes.FindAsync(id);
            if (entity == null) return false;

            entity.TipoLembrete = dto.TipoLembrete;
            entity.Frequencia = dto.Frequencia;
            entity.Ativo = dto.Ativo;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Lembretes.FindAsync(id);
            if (entity == null) return false;

            _context.Lembretes.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
}