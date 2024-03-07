using Library.Business.Models.Dtos.LoginUser;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface ILoginUserService
    {
        Task<ResultService> Add(LoginUserCreateDto entity);
        Task<ResultService> Update(LoginUserUpdateDto entity);
        Task<ResultService> Delete(int id);
        Task<ResultService<List<LoginUserDto>>> GetAll(PagedBaseRequest request);
        Task<ResultService<LoginUserDto>> GetById(int id);
    }
}
