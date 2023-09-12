using Locadora.API.Models;

namespace Locadora.API.Data {
    public interface IRepository {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        bool SaveChanges();
        void Delete<T>(T entity) where T : class;
        
        // Users
        Users[] GetAllUsers();
        Users GetUserById(int userId);
        
        // Books
        Books[] GetAllBooks(bool includePublisher = false);
        Books GetBookById(int bookId, bool includePublisher = false);
        Books[] GetAllBooksByPublisherId(int publisherId, bool includePublisher = false);

        // Publishers
        Publishers[] GetAllPublishers();
        Publishers GetPublisherById(int publisherId);

        // Rentals
        Rentals[] GetAllRentals(bool includeUser = false, bool includeBook = false);
        Rentals GetRentalById(int rentalId, bool includeUser = false, bool includeBook = false);
        Rentals[] GetAllRentalsByUserId(int userId, bool includeUser = false);
        Rentals[] GetAllRentalsByBookId(int bookId, bool includeBook = false);

    }
}
