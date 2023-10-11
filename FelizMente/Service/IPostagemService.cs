using FelizMente.Model;

namespace FelizMente.Service
{
    public interface IPostagemService
    {
        Task<IEnumerable<Postagem>> GetAll();
        Task<Postagem?> GetById(long id);
        Task<IEnumerable<Postagem>> GetByTitulo(string titulo);
        Task<IEnumerable<Postagem>> GetByEstado(string estado);
        Task<Postagem?> Create(Postagem postagem);
        Task<Postagem?> Update(Postagem postagem);
        Task Delete(Postagem postagem);
    }
}
