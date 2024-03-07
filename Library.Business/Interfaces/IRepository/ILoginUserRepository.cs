using Library.Business.Models;
using Library.Business.Pagination;

namespace Library.Business.Interfaces.IRepository
{
    public interface ILoginUserRepository : IRepository<LoginUsers>
    {
        Task<PagedBaseResponse<LoginUsers>> GetAll(PagedBaseRequest request);
        Task<LoginUsers> GetById(int id);
    }
}
