using Locadora.API.Models;

namespace Locadora.API.Data {
    public interface IRepository {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        bool SaveChanges();
        void Delete<T>(T entity) where T : class;
        
        // Users
        Task<Users[]> GetAllUsers();
        Task<Users> GetUserById(int userId);
        
        // Books
        Task<Books[]> GetAllBooks(bool includePublisher = false);
        Task<Books> GetBookById(int bookId, bool includePublisher = false);
        Task<Books[]> GetAllBooksByPublisherId(int publisherId);

        // Publishers
        Task<Publishers[]> GetAllPublishers();
        Task<Publishers> GetPublisherById(int publisherId);
        Task<Publishers> GetPublisherByName(string publisherName);

        // Rentals
        Task<Rentals[]> GetAllRentals(bool includeBook = false, bool includeUser = false);
        Task<Rentals> GetRentalById(int rentalId, bool includeBook = false, bool includeUser = false);
        Task<Rentals[]> GetAllRentalsByUserId(int userId);
        Task<Rentals[]> GetAllRentalsByBookId(int bookId);
    }
}
