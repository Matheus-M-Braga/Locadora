using Locadora.API.Models;
using Locadora.API.Data;
using Microsoft.EntityFrameworkCore;

namespace Locadora.API.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly DataContext _context;
        public BookRepository(DataContext context)
        {
            _context = context;
        }

        async Task IBookRepository.Add<T>(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        async Task IBookRepository.Update<T>(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        async Task<bool> IBookRepository.SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        async Task IBookRepository.Delete<T>(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<Books[]> GetAllBooks(bool includePublisher = false)
        {
            IQueryable<Books> query = _context.Books;
            if (includePublisher)
            {
                query = query.Include(b => b.Publisher);
            }
            query = query.AsNoTracking().OrderBy(b => b.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Books> GetBookById(int bookId, bool includePublisher = false)
        {
            IQueryable<Books> query = _context.Books;
            if (includePublisher)
            {
                query = query.Include(b => b.Publisher);
            }
            query = query.AsNoTracking().OrderBy(b => b.Id).Where(book => book.Id == bookId);
            return await query.FirstOrDefaultAsync();
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
            {
                result.Add(new Books[] { book });
            }

            return result;
        }

        public async Task<Books> UpdateQuantity(int id)
        {
            IQueryable<Books> query = _context.Books;

            query = query.AsNoTracking().Where(b => b.Id == id);

            return null; 
        }
    }
}