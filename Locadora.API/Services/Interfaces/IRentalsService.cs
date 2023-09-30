using Locadora.API.Dtos;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces
{
    public interface IRentalsService
    {
        Task<ResultService<ICollection<RentalsDto>>> GetAll();
        Task<ResultService<RentalsDto>> GetById(int id);
        Task<ResultService> Create(CreateRentalDto model);
        Task<ResultService> Update(UpdateRentalDto model);
        Task<ResultService> Delete(int id);
    }
}
