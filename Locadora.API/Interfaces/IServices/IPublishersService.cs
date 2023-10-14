using Locadora.API.Dtos.Publisher;
using Locadora.API.Models;
using Locadora.API.Pagination;
using Locadora.API.Services;

namespace Locadora.API.Interfaces.IServices
{
    public interface IPublishersService
    {
        Task<ResultService<List<Publishers>>> GetAll(FilterDb filterDb);
        Task<ResultService<Publishers>> GetById(int id);
        Task<ResultService<ICollection<PublisherBookDto>>> GetAllSelect();
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(UpdatePublisherDto model);
        Task<ResultService> Delete(int id);
    }
}
