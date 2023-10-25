using Microsoft.EntityFrameworkCore;
using Library.Data.Context;
using Library.Business.Pagination;
using Library.Business.Models;
using Library.Business.Interfaces.IRepository;

namespace Library.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Users entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Users entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Users entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedBaseResponse<Users>> GetAllUsersPaged(FilterDb request)
        {
            var users = _context.Users.AsQueryable();
            if (request.FilterValue != null)
            {
                var search = request.FilterValue.ToLower();
                users = users.Where(
                    u => u.Id.ToString().Contains(search) ||
                    u.Name.ToLower().Contains(search) ||
                    u.City.ToLower().Contains(search) ||
                    u.Address.ToLower().Contains(search) ||
                    u.Email.ToLower().Contains(search)
                );
            }
            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Users>, Users>(users, request);
        }

        public async Task<List<Users>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<Users> GetUserById(int userId)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == userId);
        }

        public async Task<List<Users>> GetUserByEmail(string email)
        {
            return await _context.Users.Where(u => u.Email == email).ToListAsync();
        }
    }
}
