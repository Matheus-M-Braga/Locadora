using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Services.Interfaces
{
    public interface IBooksService
    {
        Task<ResultService<PagedBaseResponseDto<Books>>> GetAll(FilterDb filterDb);
        Task<ResultService<BooksDto>> GetById(int id);
        Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect();
        Task<ResultService> Create(CreateBookDto model);
        Task<ResultService> Update(UpdateBookDto model);
        Task<ResultService> Delete(int id);
    }
}
