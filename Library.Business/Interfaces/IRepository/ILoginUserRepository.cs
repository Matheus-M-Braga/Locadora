using Library.Business.Models;

namespace Library.Business.Interfaces.IRepository
{
    public interface ILoginUserRepository : IRepository<LoginUsers>
    {
        Task<List<LoginUsers>> GetAll();
        Task<LoginUsers> GetById(int id);
    }
}
