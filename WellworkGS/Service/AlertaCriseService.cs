using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class AlertaCriseService
{
    private readonly AppDbContext _context;

        public AlertaCriseService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AlertaCriseReadDTO>> GetAllAsync()
        {
            return await _context.AlertasCrise
                .Select(a => new AlertaCriseReadDTO
                {
                    IdAlertaCrise = a.IdAlertaCrise,
                    IdUsuario = a.IdUsuario,
                    IdGestor = a.IdGestor,
                    DataHoraAlerta = a.DataHoraAlerta,
                    StatusAlerta = a.StatusAlerta
                })
                .ToListAsync();
        }

        public async Task<AlertaCriseReadDTO?> GetByIdAsync(int id)
        {
            var entity = await _context.AlertasCrise.FindAsync(id);
            return entity == null
                ? null
                : new AlertaCriseReadDTO
                {
                    IdAlertaCrise = entity.IdAlertaCrise,
                    IdUsuario = entity.IdUsuario,
                    IdGestor = entity.IdGestor,
                    DataHoraAlerta = entity.DataHoraAlerta,
                    StatusAlerta = entity.StatusAlerta
                };
        }

        public async Task<AlertaCriseReadDTO> CreateAsync(AlertaCriseCreateDTO dto)
        {
            var entity = new AlertaCrise
            {
                IdUsuario = dto.IdUsuario,
                IdGestor = dto.IdGestor,
                DataHoraAlerta = DateTime.Now,
                StatusAlerta = dto.StatusAlerta
            };

            _context.AlertasCrise.Add(entity);
            await _context.SaveChangesAsync();

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
            var entity = await _context.AlertasCrise.FindAsync(id);
            if (entity == null) return false;

            entity.StatusAlerta = dto.StatusAlerta;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.AlertasCrise.FindAsync(id);
            if (entity == null) return false;

            _context.AlertasCrise.Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        }
}