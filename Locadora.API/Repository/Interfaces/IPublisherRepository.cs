using Locadora.API.Dtos;
using Locadora.API.FiltersDb;
using Locadora.API.Helpers;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public interface IPublisherRepository
    {
        Task<Publishers> Add(Publishers entity);
        Task Update(Publishers entity);
        Task Delete(Publishers entity);

        Task<List<Publishers>> GetAllPublishers();
        Task<PagedBaseResponse<Publishers>> GetPaged(PublisherFilterDb request);
        Task<Publishers> GetPublisherById(int publisherId);
        Task<List<Publishers>> GetPublisherByName(string publisherName);
    }
}