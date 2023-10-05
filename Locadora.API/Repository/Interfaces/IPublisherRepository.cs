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

        Task<PagedBaseResponse<Publishers>> GetAllPublishersPaged(FilterDb request);
        Task<List<Publishers>> GetAllPublishers();
        Task<Publishers> GetPublisherById(int publisherId);
        Task<List<Publishers>> GetPublisherByName(string publisherName);
    }
}