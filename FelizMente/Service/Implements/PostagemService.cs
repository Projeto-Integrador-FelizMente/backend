using FelizMente.Data;
using FelizMente.Model;
using Microsoft.EntityFrameworkCore;

namespace FelizMente.Service.Implements
{
    public class PostagemService : IPostagemService
    {
        private readonly AppDbContext _context;

        public PostagemService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Postagem>> GetAll()
        {
            return await _context.Postagens
                .Include(p => p.Tema)
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<Postagem?> GetById(long id)
        {
            try
            {
                var Postagem = await _context.Postagens
                    .Include(p => p.Tema)
                    .Include(p => p.User)
                    .FirstAsync(p => p.Id == id);
                return Postagem;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<Postagem>> GetByTitulo(string titulo)
        {
            var Postagem = await _context.Postagens
              .Include(p => p.Tema)
              .Include(p => p.User)
              .Where(p => p.Titulo.Contains(titulo))
              .ToListAsync();
            return Postagem;
        }

        public async Task<IEnumerable<Postagem>> GetByEstado(bool estado)
        {
            var Postagem = await _context.Postagens
                         .Include(p => p.Tema)
                         .Include(p => p.User)
                         .Where(p => p.Estado == estado)
                         .ToListAsync();
            return Postagem;
        }

        public async Task<Postagem?> Create(Postagem postagem)
        {
            if (postagem.Tema is not null )
            {
                var BuscarFK = await _context.Temas.FindAsync(postagem.Tema.Id);
               
                if (BuscarFK is null)
                    return null;


                postagem.Tema = BuscarFK;

            }

            postagem.User = postagem.User is not null ? await _context.Users.FirstOrDefaultAsync(u => u.Id == postagem.User.Id) : null;

            await _context.Postagens.AddAsync(postagem);
            await _context.SaveChangesAsync();
            return postagem;
        }

        public async Task<Postagem?> Update(Postagem postagem)
        {
            var PostagemUpdate = await _context.Postagens.FindAsync(postagem.Id);

            if (PostagemUpdate is null)
                return null;

            if (postagem.Tema is not null)
            {
                var BuscarFK = await _context.Temas.FindAsync(postagem.Tema.Id);             

                if (BuscarFK is null)
                    return null;

                postagem.Tema = BuscarFK;
            }

            postagem.User = postagem.User is not null ? await _context.Users.FirstOrDefaultAsync(u => u.Id == postagem.User.Id) : null;

            _context.Entry(PostagemUpdate).State = EntityState.Detached;
            _context.Entry(postagem).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return postagem;
        }

        public async Task Delete(Postagem postagem)
        {
            _context.Remove(postagem);
            await _context.SaveChangesAsync();
        }
    }
}
