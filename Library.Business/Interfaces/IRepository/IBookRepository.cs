using Library.Business.Models;
using Library.Business.Pagination;
//
namespace Library.Business.Interfaces.IRepository
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
        Task<List<Books>> GetAllBooksByPublisherId(int publisherId);
        Task<bool> UpdateQuantity(int id, bool IsUpdate = false);
    }
}