using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Business.Pagination;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public class LoginUserRepository : Repository<LoginUsers>, ILoginUserRepository
    {
        public LoginUserRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedBaseResponse<LoginUsers>> GetAll(PagedBaseRequest request)
        {
            var loginUsers = _context.LoginUsers.AsQueryable();
            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<LoginUsers>, LoginUsers>(loginUsers, request);
        }

        public async Task<LoginUsers> GetById(int id)
        {
            return await _context.LoginUsers.FirstOrDefaultAsync(lu => lu.Id == id);
        }
    }
}
