using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Business.Pagination;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public class UserRepository : Repository<Users>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
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
