using Library.Business.Models.Dtos.Publisher;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IPublishersService
    {
        Task<ResultService<List<PublisherDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<PublisherListDto>>> GetSummary();
        Task<ResultService<PublisherDto>> GetById(int id);
        Task<ResultService> Create(CreatePublisherDto model);
        Task<ResultService> Update(UpdatePublisherDto model);
        Task<ResultService> Delete(int id);
    }
}
