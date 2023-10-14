using Locadora.API.Dtos.Book;
using Locadora.API.Pagination;
using Locadora.API.Services;

namespace Locadora.API.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<ResultService<List<BookDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<BookDashDto>>> GetAllDash();
        Task<ResultService<BookDto>> GetById(int id);
        Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect();
        Task<ResultService> Create(CreateBookDto model);
        Task<ResultService> Update(UpdateBookDto model);
        Task<ResultService> Delete(int id);
    }
}
