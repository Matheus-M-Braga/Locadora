using Locadora.API.Dtos;
using Locadora.API.Services;
using Locadora.API.Models;

namespace Locadora.API.Services.Interfaces
{
    public interface IPublishersService
    {
        Task<ResultService<ICollection<Publishers>>> GetAll();
        Task<ResultService<Publishers>> GetById(int id);
        Task<ResultService<ICollection<PublisherBookDto>>> GetAllSelect();
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(Publishers model);
        Task<ResultService> Delete(int id);
    }
}
