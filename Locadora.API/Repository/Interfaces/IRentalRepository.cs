using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Pagination;

namespace Locadora.API.Repository.Interfaces {
    public interface IRentalRepository {
        Task Add(Rentals entity);
        Task Update(Rentals entity);
        Task Delete(Rentals entity);

        Task<PagedBaseResponse<Rentals>> GetAllRentals(FilterDb request);
        Task<List<Rentals>> GetAllRentalsDash();
        Task<Rentals> GetRentalById(int rentalId);
        Task<List<Rentals[]>> GetAllRentalsByUserId(int userId);
        Task<List<Rentals[]>> GetAllRentalsByBookId(int bookId);
        Task<List<Rentals[]>> GetRentalByUserIdandBookId(int bookId, int userId);
        Task<bool> CheckDate(DateTime date);
        Task<bool?> CheckForecastDate(DateTime forecastDate, DateTime rentalDate);
        Task<string> GetStatus(DateTime ForecastDate, DateTime? ReturnDate);
    }
}