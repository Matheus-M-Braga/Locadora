using Locadora.API.Dtos;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces
{
    public interface IBooksService
    {
        Task<ResultService<ICollection<BooksDto>>> GetAsync();
        Task<ResultService<BooksDto>> GetByIdAsync(int id);
        Task<ResultService> CreateAsync(CreateBookDto model);
        Task<ResultService> UpdateAsync(UpdateBookDto model);
        Task<ResultService> DeleteAsync(int id);
    }
}
