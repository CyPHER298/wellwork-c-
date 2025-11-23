using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class TemporizadorService
{
    private readonly AppDbContext _context;

        public TemporizadorService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TemporizadorReadDTO>> GetAllAsync()
        {
            return await _context.Temporizadores
                .Select(t => new TemporizadorReadDTO
                {
                    IdTemporizador = t.IdTemporizador,
                    IdUsuario = t.IdUsuario,
                    TipoTemporizador = t.TipoTemporizador,
                    Duracao = t.Duracao,
                    Inicio = t.Inicio,
                    Fim = t.Fim,
                    StatusTemporizador = t.StatusTemporizador
                }).ToListAsync();
        }

        public async Task<TemporizadorReadDTO?> GetByIdAsync(int id)
        {
            var temporizador = await _context.Temporizadores.FindAsync(id);
            return temporizador == null
                ? null
                : new TemporizadorReadDTO
                {
                    IdTemporizador = temporizador.IdTemporizador,
                    IdUsuario = temporizador.IdUsuario,
                    TipoTemporizador = temporizador.TipoTemporizador,
                    Duracao = temporizador.Duracao,
                    Inicio = temporizador.Inicio,
                    Fim = temporizador.Fim,
                    StatusTemporizador = temporizador.StatusTemporizador
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

            _context.Temporizadores.Add(entity);
            await _context.SaveChangesAsync();

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
            var entity = await _context.Temporizadores.FindAsync(id);
            if (entity == null) return false;

            entity.TipoTemporizador = dto.TipoTemporizador;
            entity.Duracao = dto.Duracao;
            entity.Inicio = dto.Inicio;
            entity.Fim = dto.Fim;
            entity.StatusTemporizador = dto.StatusTemporizador;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.Temporizadores.FindAsync(id);
            if (entity == null) return false;

            _context.Temporizadores.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
}