using Library.Business.Models;
using Library.Business.Models.Dtos.User;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IUsersService
    {
        Task<ResultService<List<Users>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<UserRentalDto>>> GetAllSelect();
        Task<ResultService<Users>> GetById(int id);
        Task<ResultService> Create(CreateUserDto model);
        Task<ResultService> Update(UpdateUserDto model);
        Task<ResultService> Delete(int id);
    }
}
