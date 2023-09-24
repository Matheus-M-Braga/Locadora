using Locadora.API.Models;

namespace Locadora.API.Repository
{
    public interface IBookRepository
    {
        Task Add<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task Delete<T>(T entity) where T : class;

        Task<Books[]> GetAllBooks(bool includePublisher = false);
        Task<Books> GetBookById(int bookId, bool includePublisher = false);
        Task<List<Books>> GetBookByName(string bookName);
        Task<List<Books[]>> GetAllBooksByPublisherId(int publisherId);
    }
}