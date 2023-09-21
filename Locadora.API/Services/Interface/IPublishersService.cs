using Locadora.API.Dtos;
using Locadora.API.Services;
using Locadora.API.Models;

namespace Locadora.API.Services.Interface
{
    public interface IPublishersService
    {
        Task<ResultService<ICollection<Publishers>>> GetAsync();
        Task<ResultService<Publishers>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(CreatePublisherDto model);
        Task<ResultService> UpdateAsync(Publishers model);
        Task<ResultService> DeleteAsync(int id);
    }
}
