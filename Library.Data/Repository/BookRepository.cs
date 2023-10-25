using Microsoft.EntityFrameworkCore;
using Library.Data.Context;
using Library.Business.Pagination;
using Library.Business.Models;
using Library.Business.Interfaces.IRepository;

namespace Library.Data.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Books entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Books entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Books entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
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

        public async Task<List<Books>> GetAllBooks()
        {
            return await _context.Books.Include(b => b.Publisher).Where(b => b.Quantity > 0).ToListAsync();
        }

        public async Task<Books> GetBookById(int bookId)
        {
            return await _context.Books.AsNoTracking().Include(b => b.Publisher).FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<List<Books>> GetBookByName(string bookName)
        {
            return await _context.Books.Where(b => b.Name == bookName).ToListAsync();
        }

        public async Task<List<Books>> GetAllBooksByPublisherId(int publisherId)
        {
            return await _context.Books.Where(b => b.PublisherId == publisherId).ToListAsync();
        }

        public async Task<bool> UpdateQuantity(int id, bool IsUpdate = false)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return false;
            }

            if (IsUpdate)
            {
                book.Quantity++;
                book.Rented--;

                if (book.Rented < 0)
                    return false;
            }
            else
            {
                book.Quantity--;
                book.Rented++;
                if (book.Quantity < 0)
                    return false;
            }

            await _context.SaveChangesAsync();
            return true;
        }
    }
}