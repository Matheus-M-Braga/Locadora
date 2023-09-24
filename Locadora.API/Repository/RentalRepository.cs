using Locadora.API.Models;
using Locadora.API.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Locadora.API.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;
        public RentalRepository(DataContext context)
        {
            _context = context;
        }
        async Task IRentalRepository.Add<T>(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        async Task IRentalRepository.Update<T>(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        async Task<bool> IRentalRepository.SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        async Task IRentalRepository.Delete<T>(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<Rentals[]> GetAllRentals(bool includeBook = false, bool includeUser = false)
        {
            IQueryable<Rentals> query = _context.Rentals;
            if (includeUser && includeBook)
            {
                query = query.Include(r => r.User).Include(r => r.Book);
            }

            query = query.AsNoTracking().OrderBy(r => r.Id);
            return await query.ToArrayAsync();
        }

        public async Task<Rentals> GetRentalById(int rentalId, bool includeBook = false, bool includeUser = false)
        {
            IQueryable<Rentals> query = _context.Rentals;
            if (includeUser && includeBook)
            {
                query = query.Include(r => r.User).Include(r => r.Book);
            }

            query = query.AsNoTracking().OrderBy(r => r.Id).Where(rental => rental.Id == rentalId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Rentals[]>> GetAllRentalsByUserId(int userId)
        {
            var rentals = await _context.Rentals.Where(r => r.UserId == userId).ToListAsync();
            var result = new List<Rentals[]>();

            foreach (var rental in rentals)
            {
                result.Add(new Rentals[] { rental });
            }

            return result;
        }

        public async Task<List<Rentals[]>> GetAllRentalsByBookId(int bookId)
        {
            var rentals = await _context.Rentals.Where(r => r.BookId == bookId).ToListAsync();
            var result = new List<Rentals[]>();

            foreach (var rental in rentals)
            {
                result.Add(new Rentals[] { rental });
            }

            return result;
        }

        public Task<bool> CheckRentalDate(string rentalDate)
        {
            DateTime today = DateTime.Now.Date;
            string format = "yyyy-MM-dd";

            if (DateTime.TryParseExact(rentalDate, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime rentalDateTime))
            {
                bool result = rentalDateTime != today;
                return Task.FromResult(result);
            }

            return Task.FromResult(false);
        }

    }
}