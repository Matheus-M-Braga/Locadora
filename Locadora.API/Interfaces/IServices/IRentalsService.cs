using Locadora.API.Dtos.Rental;
using Locadora.API.Pagination;
using Locadora.API.Services;

namespace Locadora.API.Interfaces.IServices
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
