using Library.Business.Models.Dtos.Rental;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IRentalsService
    {
        Task<ResultService<List<RentalDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<RentalDashDto>>> GetAllDash();
        Task<ResultService<RentalDto>> GetById(int id);
        Task<ResultService> Create(CreateRentalDto model);
        Task<ResultService> Update(UpdateRentalDto model);
        Task<ResultService> Delete(int id);
    }
}
