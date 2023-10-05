using Locadora.API.FiltersDb;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public interface IUserRepository
    {
        Task<Users> Add(Users entity);
        Task Update(Users entity);
        Task Delete(Users entity);

        Task<PagedBaseResponse<Users>> GetAllUsersPaged(FilterDb request);
        Task<List<Users>> GetAllUsers();
        Task<Users> GetUserById(int userId);
        Task<List<Users>> GetUserByEmail(string email);
    }
}
