using Library.Business.Models.Dtos.User;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<ResultService<List<UserDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<UserListDto>>> GetSummary();
        Task<ResultService<UserDto>> GetById(int id);
        Task<ResultService> Create(CreateUserDto model);
        Task<ResultService> Update(UpdateUserDto model);
        Task<ResultService> Delete(int id);
    }
}
