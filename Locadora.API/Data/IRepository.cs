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
        Books[] GetAllBooks();
        Books GetBookById(int bookId);
        Books[] GetAllBooksByPublisherId(int publisherId);

        // Publishers
        Publishers[] GetAllPublishers();
        Publishers GetPublisherById(int publisherId);
        Publishers GetPublisherByName(string publisherName);

        // Rentals
        Rentals[] GetAllRentals();
        Rentals GetRentalById(int rentalId);
        Rentals[] GetAllRentalsByUserId(int userId);
        Rentals[] GetAllRentalsByBookId(int bookId);

    }
}
