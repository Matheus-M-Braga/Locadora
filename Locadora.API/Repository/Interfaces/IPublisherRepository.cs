using Locadora.API.FiltersDb;
using Locadora.API.Helpers;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public interface IPublisherRepository
    {
        Task Add<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task Delete<T>(T entity) where T : class;

        Task<List<Publishers>> GetAllPublishers();
        Task<PagedBaseResponse<Publishers>> GetPaged(PublisherFilterDb request);
        Task<Publishers> GetPublisherById(int publisherId);
        Task<List<Publishers>> GetPublisherByName(string publisherName);
    }
}