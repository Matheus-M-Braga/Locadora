using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Services.Interfaces
{
    public interface IPublishersService
    {
        Task<ResultService<PagedBaseResponseDto<Publishers>>> GetAll(FilterDb filterDb);
        Task<ResultService<Publishers>> GetById(int id);
        Task<ResultService<ICollection<PublisherBookDto>>> GetAllSelect();
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(Publishers model);
        Task<ResultService> Delete(int id);
    }
}
