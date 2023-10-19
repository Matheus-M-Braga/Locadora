using Library.Business.Models;
using Library.Business.Pagination;

namespace Library.Business.Interfaces.IRepository
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