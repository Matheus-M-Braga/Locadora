using Locadora.API.FiltersDb;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public interface IRentalRepository
    {
        Task<Rentals> Add(Rentals entity);
        Task Update(Rentals entity);
        Task Delete(Rentals entity);

        Task<PagedBaseResponse<Rentals>> GetAllRentals(RentalFilterDb request);
        Task<Rentals> GetRentalById(int rentalId);
        Task<List<Rentals[]>> GetAllRentalsByUserId(int userId);
        Task<List<Rentals[]>> GetAllRentalsByBookId(int bookId);
        Task<List<Rentals[]>> GetRentalByUserIdandBookId(int bookId, int userId);
        Task<bool> CheckDate(string rentalDate);
        Task<bool?> CheckForecastDate(string forecastDate, string rentalDate);
        Task<bool> GetStatus(string forecastDate, string returnDate);
    }
}