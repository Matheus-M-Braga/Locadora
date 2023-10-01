using Locadora.API.Models;
using Locadora.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }
        async Task IUserRepository.Add<T>(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        async Task IUserRepository.Update<T>(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        async Task<bool> IUserRepository.SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        async Task IUserRepository.Delete<T>(T entity)
        {
           _context.Set<T>().Remove(entity);
        }

        public async Task<Users[]> GetAllUsers()
        {
            IQueryable<Users> query = _context.Users;

            query = query.AsNoTracking().OrderBy(user => user.Id);
            return await query.ToArrayAsync();
        }
        public async Task<Users> GetUserById(int userId)
        {
            IQueryable<Users> query = _context.Users;

            query = query.AsNoTracking().OrderBy(user => user.Id).Where(user => user.Id == userId);
            return await query.FirstOrDefaultAsync();
        }
        public async Task<List<Users>> GetUserByEmail(string email){
            return await _context.Users.Where(u => u.Email == email).ToListAsync();
        }
    }
}
