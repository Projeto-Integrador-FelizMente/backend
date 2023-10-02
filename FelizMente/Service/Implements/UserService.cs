using FelizMente.Data;
using FelizMente.Model;
using Microsoft.EntityFrameworkCore;

namespace FelizMente.Service.Implements
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users
                            .Include(u => u.Postagem)
                            .ToListAsync();
        }

        public async Task<User?> GetById(long id)
        {
            try
            {
                var User = await _context.Users
                            .Include(u => u.Postagem)
                            .FirstAsync(u => u.Id == id);
                return User;
            }
            catch
            {
                return null;
            }
        }

        public async Task<IEnumerable<User>> GetByNome(string nome)
        {
            var User = await _context.Users
                          .Include(u => u.Postagem)
                          .Where(u => u.Nome.Contains(nome))
                          .ToListAsync();
            return User;
        }

        public async Task<IEnumerable<User>> GetByTipo(bool tipo)
        {
            var User = await _context.Users
             .Include(u => u.Postagem)
             .Where(u => u.Tipo == tipo)
             .ToListAsync();
            return User;
        }

        public async Task<User?> Create(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> Update(User user)
        {
            var UserUpdate = await _context.Users.FindAsync(user.Id);

            if (UserUpdate is null)
                return null;

            _context.Entry(UserUpdate).State = EntityState.Detached;
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task Delete(User user)
        {
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}
