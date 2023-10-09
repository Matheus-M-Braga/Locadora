using Locadora.API.Models;
using Locadora.API.Context;
using Microsoft.EntityFrameworkCore;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<Books> Add(Books entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
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
                books = books.Where(
                    b => b.Id.ToString().Contains(request.FilterValue) ||
                    b.Name.Contains(request.FilterValue) ||
                    b.Author.Contains(request.FilterValue) ||
                    b.Release.Contains(request.FilterValue) ||
                    b.Quantity.ToString().Contains(request.FilterValue) ||
                    b.Rented.ToString().Contains(request.FilterValue) ||
                    b.PublisherId.ToString().Contains(request.FilterValue) ||
                    b.Publisher.Name.Contains(request.FilterValue)
                );

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Books>, Books>(books, request);
        }

        public async Task<List<Books>> GetAllBooks()
        {
            return await _context.Books.Include(b => b.Publisher).ToListAsync();
        }

        public async Task<Books> GetBookById(int bookId)
        {
            return await _context.Books.AsNoTracking().Include(b => b.Publisher).FirstOrDefaultAsync(b => b.Id == bookId);
        }

        public async Task<List<Books>> GetBookByName(string bookName)
        {
            return await _context.Books.Where(b => b.Name == bookName).ToListAsync();
        }

        public async Task<List<Books[]>> GetAllBooksByPublisherId(int publisherId)
        {
            var books = await _context.Books.Where(b => b.PublisherId == publisherId).ToListAsync();
            var result = new List<Books[]>();

            foreach (var book in books)
                result.Add(new Books[] { book });

            return result;
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