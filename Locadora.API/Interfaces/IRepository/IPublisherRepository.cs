using Locadora.API.Models;
using Locadora.API.Pagination;

namespace Locadora.API.Interfaces.IRepository
{
    public interface IPublisherRepository
    {
        Task Add(Publishers entity);
        Task Update(Publishers entity);
        Task Delete(Publishers entity);

        Task<PagedBaseResponse<Publishers>> GetAllPublishersPaged(FilterDb request);
        Task<List<Publishers>> GetAllPublishers();
        Task<Publishers> GetPublisherById(int publisherId);
        Task<List<Publishers>> GetPublisherByName(string publisherName);
    }
}