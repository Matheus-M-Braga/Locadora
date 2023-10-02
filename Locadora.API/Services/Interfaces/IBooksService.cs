using Locadora.API.Dtos;
using Locadora.API.Models;
using Locadora.API.Services;

namespace Locadora.API.Services.Interfaces
{
    public interface IBooksService
    {
        Task<ResultService2<ICollection<BooksDto>>> GetAll();
        Task<ResultService2<BooksDto>> GetById(int id);
        Task<ResultService2<ICollection<BookRentalDto>>> GetAllSelect();
        Task<ResultService2<Books>> Create(CreateBookDto model);
        Task<ResultService2<Books>> Update(UpdateBookDto model);
        Task<ResultService2<Books>> Delete(int id);
    }
}
