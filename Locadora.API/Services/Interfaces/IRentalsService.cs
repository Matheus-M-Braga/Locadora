using Locadora.API.Dtos;
using Locadora.API.FiltersDb;

namespace Locadora.API.Services.Interfaces
{
    public interface IRentalsService
    {
        Task<ResultService<PagedBaseResponseDto<RentalsDto>>> GetAll(RentalFilterDb rentalFilterDb);
        Task<ResultService<RentalsDto>> GetById(int id);
        Task<ResultService> Create(CreateRentalDto model);
        Task<ResultService> Update(UpdateRentalDto model);
        Task<ResultService> Delete(int id);
    }
}
