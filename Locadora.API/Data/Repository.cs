using Locadora.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using SQLitePCL;
using System.Net;

namespace Locadora.API.Data {
    public class Repository : IRepository {
        private readonly DataContext _context;
        public Repository(DataContext context) {
            _context = context;
        }
        public void Add<T>(T entity) where T : class {
            _context.Add(entity);
        }
        public void Update<T>(T entity) where T : class {
            _context.Update(entity);
        }
        public bool SaveChanges() {
           return (_context.SaveChanges() > 0);
        }
        public void Delete<T>(T entity) where T : class {
            _context.Remove(entity);
        }

        // Users
        public Users[] GetAllUsers() {
            IQueryable<Users> query = _context.Users;
            
            query = query.AsNoTracking().OrderBy(user => user.Id);
            return query.ToArray();
        }
         
        public Users GetUserById(int userId, bool includeRentals = false) {
            IQueryable<Users> query = _context.Users;
            if (includeRentals) {
                query = query.Include(u => u.Rentals);
            }

            query = query.AsNoTracking().OrderBy(user => user.Id).Where(user => user.Id == userId);
            return query.FirstOrDefault();
        }

        // Books
        public Books[] GetAllBooks(bool includePublisher = false) {
            IQueryable<Books> query = _context.Books;

            if(includePublisher) {
                query = query.Include(b => b.Publisher);
            }

            query = query.AsNoTracking().OrderBy(b => b.Id);
            return query.ToArray();
        }

        public Books GetBookById(int bookId, bool includePublisher = false) {
            IQueryable<Books> query = _context.Books;

            if (includePublisher) {
                query = query.Include(b => b.Publisher);
            }

            query = query.AsNoTracking().OrderBy(b => b.Id).Where(book => book.Id == bookId);
            return query.FirstOrDefault();
        }

        public Books[] GetAllBooksByPublisherId(int publisherId, bool includePublisher = false) {
            IQueryable<Books> query = _context.Books;

            if (includePublisher) {
                query = query.Include(b => b.Publisher).ThenInclude(p => p.Name);
            }

            query = query.AsNoTracking().OrderBy(b => b.Id).Where(book => book.PublisherId == publisherId);
            return query.ToArray();
        }

        // Publishers
        public Publishers[] GetAllPublishers() {
            IQueryable<Publishers> query = _context.Publishers;

            query = query.AsNoTracking().OrderBy(publisher => publisher.Id);
            return query.ToArray();
        }

        public Publishers GetPublisherById(int publisherId, bool includeBooks = false) {
            IQueryable<Publishers> query = _context.Publishers;
            if (includeBooks) {
                query = query.Include(p => p.Books);
            }

            query = query.AsNoTracking().OrderBy(publisher => publisher.Id).Where(publisher => publisher.Id == publisherId);
            return query.FirstOrDefault();
        }

        // Rentals
        public Rentals[] GetAllRentals(bool includeUser = false, bool includeBook = false) {
            IQueryable<Rentals> query = _context.Rentals;

            if (includeUser && includeBook) {
                query = query.Include(r => r.User).Include(r => r.Book);
            }

            query = query.AsNoTracking().OrderBy(r => r.Id);
            return query.ToArray();
        }

        public Rentals GetRentalById(int rentalId, bool includeUser = false, bool includeBook = false) {
            IQueryable<Rentals> query = _context.Rentals;

            if (includeUser && includeBook) {
                query = query.Include(r => r.User).Include(r => r.Book);
            }

            query = query.AsNoTracking().OrderBy(r => r.Id).Where(rental => rental.Id == rentalId);
            return query.FirstOrDefault();
        }

        public Rentals[] GetAllRentalsByUserId(int userId, bool includeUser = false) {
            IQueryable<Rentals> query = _context.Rentals;

            if (includeUser) {
                query = query.Include(r => r.User);
            }

            query = query.AsNoTracking().OrderBy(r => r.Id).Where(rental => rental.UserId == userId);
            return query.ToArray();
        }

        public Rentals[] GetAllRentalsByBookId(int bookId, bool includeBook = false) {
            IQueryable<Rentals> query = _context.Rentals;

            if (includeBook) {
                query = query.Include(r => r.Book);
            }

            query = query.AsNoTracking().OrderBy(r => r.Id).Where(rental => rental.BookId == bookId);
            return query.ToArray();
        }
    }
}
