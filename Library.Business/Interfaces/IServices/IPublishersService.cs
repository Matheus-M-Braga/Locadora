using Library.Business.Models;
using Library.Business.Models.Dtos.Publisher;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IPublishersService
    {
        Task<ResultService<List<Publishers>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<PublisherBookDto>>> GetAllSelect();
        Task<ResultService<Publishers>> GetById(int id);
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(UpdatePublisherDto model);
        Task<ResultService> Delete(int id);
    }
}
