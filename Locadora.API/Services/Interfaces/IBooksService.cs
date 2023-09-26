using Locadora.API.Dtos;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces
{
    public interface IBooksService
    {
        Task<ResultService<ICollection<BooksDto>>> GetAll();
        Task<ResultService<BooksDto>> GetById(int id);
        Task<ResultService> Create(CreateBookDto model);
        Task<ResultService> Update(UpdateBookDto model);
        Task<ResultService> Delete(int id);
    }
}
