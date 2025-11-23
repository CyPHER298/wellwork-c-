using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Service;

public class UsuarioService
    {
        private readonly AppDbContext _context;

        public UsuarioService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UsuarioReadDTO>> GetAllAsync()
        {
            return await _context.Usuarios
                .Select(u => new UsuarioReadDTO
                {
                    IdUsuario = u.IdUsuario,
                    NomeUsuario = u.NomeUsuario,
                    EmailUsuario = u.EmailUsuario,
                    CargoUsuario = u.CargoUsuario,
                    Acessibilidade = u.Acessibilidade
                })
                .ToListAsync();
        }

        public async Task<UsuarioReadDTO?> GetByIdAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            return user == null
                ? null
                : new UsuarioReadDTO
                {
                    IdUsuario = user.IdUsuario,
                    NomeUsuario = user.NomeUsuario,
                    EmailUsuario = user.EmailUsuario,
                    CargoUsuario = user.CargoUsuario,
                    Acessibilidade = user.Acessibilidade
                };
        }

        public async Task<UsuarioReadDTO> CreateAsync(UsuarioCreateDTO dto)
        {
            var entity = new Usuario
            {
                NomeUsuario = dto.NomeUsuario,
                EmailUsuario = dto.EmailUsuario,
                SenhaUsuario = dto.SenhaUsuario,
                CargoUsuario = dto.CargoUsuario,
                Acessibilidade = dto.Acessibilidade
            };

            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync();

            return new UsuarioReadDTO
            {
                IdUsuario = entity.IdUsuario,
                NomeUsuario = entity.NomeUsuario,
                EmailUsuario = entity.EmailUsuario,
                CargoUsuario = entity.CargoUsuario,
                Acessibilidade = entity.Acessibilidade
            };
        }

        public async Task<bool> UpdateAsync(int id, UsuarioUpdateDTO dto)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return false;

            user.NomeUsuario = dto.NomeUsuario;
            user.EmailUsuario = dto.EmailUsuario;
            user.CargoUsuario = dto.CargoUsuario;
            user.Acessibilidade = dto.Acessibilidade;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Usuarios.FindAsync(id);
            if (user == null) return false;

            _context.Usuarios.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }