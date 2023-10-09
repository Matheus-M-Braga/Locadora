using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Services.Interfaces
{
    public interface IUsersService
    {
        Task<ResultService<PagedBaseResponseDto<Users>>> GetAll(FilterDb filterDb);
        Task<ResultService<Users>> GetById(int id);
        Task<ResultService<ICollection<UserRentalDto>>> GetAllSelect();
        Task<ResultService> Create(CreateUserDto model);
        Task<ResultService> Update(Users model);
        Task<ResultService> Delete(int id);
    }
}
