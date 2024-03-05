using Library.Business.Models;
using Library.Business.Pagination;

namespace Library.Business.Interfaces.IRepository
{
    public interface IPublisherRepository : IRepository<Publishers>
    {
        Task<PagedBaseResponse<Publishers>> GetAllPublishersPaged(FilterDb request);
        Task<List<Publishers>> GetAllPublishers();
        Task<Publishers> GetPublisherById(int publisherId);
        Task<List<Publishers>> GetPublisherByName(string publisherName);
    }
}