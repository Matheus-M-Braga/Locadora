using Locadora.API.Dtos;
using Locadora.API.Pagination;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces {
    public interface IRentalsService {
        Task<ResultService<List<RentalsDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<RentalDashDto>>> GetAllDash();
        Task<ResultService<RentalsDto>> GetById(int id);
        Task<ResultService> Create(CreateRentalDto model);
        Task<ResultService> Update(UpdateRentalDto model);
        Task<ResultService> Delete(int id);
    }
}
