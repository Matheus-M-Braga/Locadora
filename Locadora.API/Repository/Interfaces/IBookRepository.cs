using Locadora.API.FiltersDb;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public interface IBookRepository
    {
        Task<Books> Add(Books entity);
        Task Update(Books entity);
        Task Delete(Books entity);

        Task<PagedBaseResponse<Books>> GetAllBooks(BookFilterDb request);
        Task<Books> GetBookById(int bookId);
        Task<List<Books>> GetBookByName(string bookName);
        Task<List<Books[]>> GetAllBooksByPublisherId(int publisherId);
        Task<bool> UpdateQuantity(int id, bool IsUpdate = false);
    }
}