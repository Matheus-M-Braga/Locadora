using Locadora.API.Dtos;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces
{
    public interface IRentalsService
    {
        Task<ResultService<ICollection<RentalsDto>>> GetAsync();
        Task<ResultService<RentalsDto>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(CreateRentalDto model);
        Task<ResultService> UpdateAsync(RentalReturnDto model);
        Task<ResultService> DeleteAsync(int id);
    }
}
