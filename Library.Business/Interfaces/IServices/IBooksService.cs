﻿using Library.Business.Models.Dtos.Book;
using Library.Business.Pagination;
using Library.Business.Services;

namespace Library.Business.Interfaces.IServices
{
    public interface IBooksService
    {
        Task<ResultService<List<BookDto>>> GetAll(FilterDb filterDb);
        Task<ResultService<ICollection<BookRentalDto>>> GetAllSelect();
        Task<ResultService<List<BookCountDto>>> GetAllCount();
        Task<ResultService<List<BookRentedDto>>> GetMostRented();
        Task<ResultService<BookDto>> GetById(int id);
        Task<ResultService> Create(CreateBookDto model);
        Task<ResultService> Update(UpdateBookDto model);
        Task<ResultService> Delete(int id);
    }
}
