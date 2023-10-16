using Locadora.API.Models;
using Locadora.API.Pagination;

namespace Locadora.API.Interfaces.IRepository
{
    public interface IBookRepository
    {
        Task Add(Books entity);
        Task Update(Books entity);
        Task Delete(Books entity);

        Task<PagedBaseResponse<Books>> GetAllBooksPaged(FilterDb request);
        Task<List<Books>> GetAllBooks();
        Task<Books> GetBookById(int bookId);
        Task<List<Books>> GetBookByName(string bookName);
        Task<List<Books[]>> GetAllBooksByPublisherId(int publisherId);
        Task<bool> UpdateQuantity(int id, bool IsUpdate = false);
    }
}