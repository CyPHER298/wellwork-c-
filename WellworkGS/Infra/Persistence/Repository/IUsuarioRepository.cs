using WellworkGS.Infra.Persistence.Models;

namespace WellworkGS.Infra.Persistence.Repository;

public interface IUsuarioRepository
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllAsync();
        Task<Usuario?> GetByIdAsync(int id);
        Task AddAsync(Usuario usuario);
        Task DeleteAsync(int id);
        void Update(Usuario usuario);
        Task<int> SaveChangesAsync();
    }
}