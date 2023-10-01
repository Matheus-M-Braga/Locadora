using Locadora.API.Dtos;
using Locadora.API.Services;
using Locadora.API.Models;
using Locadora.API.Helpers;

namespace Locadora.API.Services.Interfaces
{
    public interface IPublishersService
    {
        Task<ResultService<ICollection<Publishers>>> GetAll(PageParams pageParams);
        Task<ResultService<Publishers>> GetById(int id);
        Task<ResultService<ICollection<PublisherBookDto>>> GetAllSelect(PageParams pageParams);
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(Publishers model);
        Task<ResultService> Delete(int id);
    }
}
