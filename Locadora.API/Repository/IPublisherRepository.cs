using Locadora.API.Models;

namespace Locadora.API.Repository
{
    public interface IPublisherRepository
    {
        Task Add<T>(T entity) where T : class;
        Task Update<T>(T entity) where T : class;
        Task<bool> SaveChanges();
        Task Delete<T>(T entity) where T : class;

        Task<Publishers[]> GetAllPublishers();
        Task<Publishers> GetPublisherById(int publisherId);
        Task<List<Publishers>> GetPublisherByName(string publisherName);
    }
}