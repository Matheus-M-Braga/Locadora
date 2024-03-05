using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public class LoginUserRepository : ILoginUserRepository
    {
        private readonly DataContext _context;
        public LoginUserRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(LoginUser entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Update(LoginUser entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(LoginUser entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<LoginUser>> GetAll()
        {
            return await _context.LoginUsers.ToListAsync();
        }

        public async Task<LoginUser> GetById(int id)
        {
            return await _context.LoginUsers.FirstOrDefaultAsync(lu => lu.Id == id);
        }

        public async Task<LoginUser> GetLoginUserByEmail(string email)
        {
            return await _context.LoginUsers.FirstOrDefaultAsync(lu => lu.Email.ToLower() == email.ToLower());
        }
    }
}
