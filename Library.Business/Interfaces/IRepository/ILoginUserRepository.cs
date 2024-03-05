using Library.Business.Models;

namespace Library.Business.Interfaces.IRepository
{
    public interface ILoginUserRepository
    {
        Task Add(LoginUser entity);
        Task Update(LoginUser entity);
        Task Delete(LoginUser entity);
        Task<List<LoginUser>> GetAll();
        Task<LoginUser> GetById(int id);
        Task<LoginUser> GetLoginUserByEmail(string email);
    }
}
