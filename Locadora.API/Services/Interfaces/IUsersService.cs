using Locadora.API.Dtos;
using Locadora.API.Services;
using Locadora.API.Models;

namespace Locadora.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResultService<ICollection<Users>>> GetAll();
        Task<ResultService<Users>> GetById(int id);
        Task<ResultService<ICollection<UserRentalDto>>> GetAllSelect();
        Task<ResultService> Create(CreateUserDto model);
        Task<ResultService> Update(Users model);
        Task<ResultService> Delete(int id);
    }
}
