using Locadora.API.Dtos;
using Locadora.API.Services;
using Locadora.API.Models;

namespace Locadora.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResultService<ICollection<Users>>> GetAsync();
        Task<ResultService<Users>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(CreateUserDto model);
        Task<ResultService> UpdateAsync(Users model);
        Task<ResultService> DeleteAsync(int id);
    }
}
