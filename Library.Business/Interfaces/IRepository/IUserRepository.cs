using Library.Business.Models;
using Library.Business.Pagination;

namespace Library.Business.Interfaces.IRepository
{
    public interface IUserRepository : IRepository<Users>
    {
        Task<PagedBaseResponse<Users>> GetAllUsersPaged(FilterDb request);
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserById(int userId);
        Task<List<Users>> GetUserByEmail(string email);
    }
}
