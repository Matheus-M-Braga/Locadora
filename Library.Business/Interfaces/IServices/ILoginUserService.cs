using Library.Business.Models;
using Library.Business.Models.Dtos.LoginUser;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface ILoginUserService
    {
        Task<ResultService> Add(LoginUserCreateDto entity);
        Task<ResultService> Update(LoginUserUpdateDto entity);
        Task<ResultService> Delete(int id);
        Task<ResultService<List<LoginUser>>> GetAll();
        Task<ResultService<LoginUser>> GetById(int id);
    }
}
