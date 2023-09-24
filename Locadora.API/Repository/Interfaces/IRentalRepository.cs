using Locadora.API.Models;

namespace Locadora.API.Repository
{
    public interface IRentalRepository
    {
        Task Add<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task Delete<T>(T entity) where T : class;

        Task<Rentals[]> GetAllRentals(bool includeBook = false, bool includeUser = false);
        Task<Rentals> GetRentalById(int rentalId, bool includeBook = false, bool includeUser = false);
        Task<List<Rentals[]>> GetAllRentalsByUserId(int userId);
        Task<List<Rentals[]>> GetAllRentalsByBookId(int bookId);
        Task<bool> CheckRentalDate(string rentalDate);
    }
}