using Locadora.API.Dtos;
using Locadora.API.Pagination;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces {
    public interface IBooksService {
        Task<ResultService<List<BooksDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<List<BookDashDto>>> GetAllDash();
        Task<ResultService<BooksDto>> GetById(int id);
        Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect();
        Task<ResultService> Create(CreateBookDto model);
        Task<ResultService> Update(UpdateBookDto model);
        Task<ResultService> Delete(int id);
    }
}
