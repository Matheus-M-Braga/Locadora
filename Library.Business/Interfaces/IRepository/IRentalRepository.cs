using Library.Business.Models;
using Library.Business.Pagination;

namespace Library.Business.Interfaces.IRepository
{
    public interface IRentalRepository : IRepository<Rentals>
    {
        Task<PagedBaseResponse<Rentals>> GetAllRentalsPaged(FilterDb request);
        Task<List<Rentals>> GetAllRentals();
        Task<Rentals> GetRentalById(int rentalId);
        Task<List<Rentals>> GetAllRentalsByUserId(int userId);
        Task<List<Rentals>> GetAllRentalsByBookId(int bookId);
        Task<List<Rentals>> GetRentalByUserIdandBookId(int bookId, int userId);
    }
}