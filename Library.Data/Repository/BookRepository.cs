using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Business.Pagination;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public class BookRepository : Repository<Books>, IBookRepository
    {
        public BookRepository(DataContext context) : base(context)
        {
        }

        public async Task<PagedBaseResponse<Books>> GetAllBooksPaged(FilterDb request)
        {
            var books = _context.Books.Include(b => b.Publisher).AsQueryable();

            if (request.FilterValue != null)
            {
                var search = request.FilterValue.ToLower();
                books = books.Where(
                        b => b.Id.ToString().Contains(search) ||
                        b.Name.ToLower().Contains(search) ||
                        b.Author.ToLower().Contains(search) ||
                        b.Release.ToString().Contains(search) ||
                        b.Quantity.ToString().Contains(search) ||
                        b.Rented.ToString().Contains(search) ||
                        b.PublisherId.ToString().Contains(search) ||
                        b.Publisher.Name.ToLower().Contains(search)
                    );
            }
            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Books>, Books>(books, request);
        }

        public async Task<List<Books>> GetSummary()
        {
            return await _context.Books.Include(b => b.Publisher).Where(b => b.Quantity > 0).ToListAsync();
        }

        public async Task<Books> GetBookById(int bookId)
        {
            return await _context.Books.AsNoTracking().Include(b => b.Publisher).FirstOrDefaultAsync(b => b.Id == bookId);
        }
    }
}