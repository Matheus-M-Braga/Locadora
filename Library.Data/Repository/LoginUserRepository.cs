using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public class LoginUserRepository : Repository<LoginUsers>, ILoginUserRepository
    {
        public LoginUserRepository(DataContext context) : base(context)
        {
        }

        public async Task<List<LoginUsers>> GetAll()
        {
            return await _context.LoginUsers.ToListAsync();
        }

        public async Task<LoginUsers> GetById(int id)
        {
            return await _context.LoginUsers.FirstOrDefaultAsync(lu => lu.Id == id);
        }
    }
}
