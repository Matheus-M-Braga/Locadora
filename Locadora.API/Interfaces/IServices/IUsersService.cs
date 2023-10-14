using Locadora.API.Dtos.User;
using Locadora.API.Models;
using Locadora.API.Pagination;
using Locadora.API.Services;

namespace Locadora.API.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<ResultService<List<Users>>> GetAll(FilterDb filterDb);
        Task<ResultService<Users>> GetById(int id);
        Task<ResultService<ICollection<UserRentalDto>>> GetAllSelect();
        Task<ResultService> Create(CreateUserDto model);
        Task<ResultService> Update(UpdateUserDto model);
        Task<ResultService> Delete(int id);
    }
}
