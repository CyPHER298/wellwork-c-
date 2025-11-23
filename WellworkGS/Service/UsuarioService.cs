using Microsoft.EntityFrameworkCore;
using WellworkGS.Data;
using WellworkGS.DTOs;
using WellworkGS.Infra.Persistence.Models;
using WellworkGS.Infra.Persistence.Repository;

namespace WellworkGS.Service;

public class UsuarioService
{
    private readonly IUsuarioRepository _repository;

    public UsuarioService(IUsuarioRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<UsuarioReadDTO>> GetAllAsync()
    {
        var users = await _repository.GetAllAsync();

        return users.Select(u => new UsuarioReadDTO
        {
            IdUsuario = u.IdUsuario,
            NomeUsuario = u.NomeUsuario,
            EmailUsuario = u.EmailUsuario,
            CargoUsuario = u.CargoUsuario,
            Acessibilidade = u.Acessibilidade
        });
    }

    public async Task<UsuarioReadDTO?> GetByIdAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null) return null;

        return new UsuarioReadDTO
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

        await _repository.AddAsync(entity);

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
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
            return false;

        user.NomeUsuario = dto.NomeUsuario;
        user.EmailUsuario = dto.EmailUsuario;
        user.CargoUsuario = dto.CargoUsuario;
        user.Acessibilidade = dto.Acessibilidade;

        _repository.Update(user);
        await _repository.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _repository.GetByIdAsync(id);
        if (user == null)
            return false;

        await _repository.DeleteAsync(id);
        return true;
    }
}