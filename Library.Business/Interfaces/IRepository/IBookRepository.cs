using Library.Business.Models;
using Library.Business.Pagination;

namespace Library.Business.Interfaces.IRepository
{
    public interface IBookRepository : IRepository<Books>
    {
        Task<PagedBaseResponse<Books>> GetAllBooksPaged(FilterDb request);
        Task<List<Books>> GetSummary();
        Task<Books> GetBookById(int bookId);
    }
}