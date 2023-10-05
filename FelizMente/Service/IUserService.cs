using FelizMente.Model;

namespace FelizMente.Service
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAll();
        Task<User?> GetById(long id);
        Task<IEnumerable<User>> GetByNome(string nome);
        Task<IEnumerable<User>> GetByTipo(bool tipo);
        Task<User?> GetByUsuario(string usuario);
        Task<User?> Create(User user);
        Task<User?> Update(User user);
    }
}
