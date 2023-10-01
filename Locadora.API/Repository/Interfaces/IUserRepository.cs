using Locadora.API.Models;

namespace Locadora.API.Repository
{
    public interface IUserRepository
    {
        Task Add<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task Delete<T>(T entity) where T : class;

        Task<Users[]> GetAllUsers();
        Task<Users> GetUserById(int userId);
        Task<List<Users>> GetUserByEmail(string email);
    }
}
