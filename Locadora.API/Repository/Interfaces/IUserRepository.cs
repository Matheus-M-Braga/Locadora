using Locadora.API.Models;

namespace Locadora.API.Repository
{
    public interface IUserRepository
    {
        Task<Users> Add(Users entity);
        Task Update<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task Delete<T>(T entity) where T : class;

        Task<Users[]> GetAllUsers();
        Task<Users> GetUserById(int userId);
        Task<List<Users>> GetUserByEmail(string email);
    }
}
