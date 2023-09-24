using Locadora.API.Dtos;
using Locadora.API.Services;

namespace Locadora.API.Services.Interface
{
    public interface IRentalsService
    {
        Task<ResultService<ICollection<RentalsDto>>> GetAsync();
        Task<ResultService<RentalsDto>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(CreateRentalDto rentalsDTO);
        Task<ResultService> UpdateAsync(RentalReturnDto rentalsUpdateDTO);
        Task<ResultService> DeleteAsync(int id);
    }
}
