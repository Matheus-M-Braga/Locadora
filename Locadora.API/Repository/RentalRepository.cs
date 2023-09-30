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
            var rentals = await _context.Rentals.AsNoTracking().Where(r => r.BookId == bookId && r.Status == "Pendente").ToListAsync();
            var result = new List<Rentals[]>();

            foreach (var rental in rentals)
            {
                result.Add(new Rentals[] { rental });
            }

            return result;
        }

        public async Task<List<Rentals[]>> GetRentalByUserIdandBookId(int bookId, int userId)
        {
            var rentals = await _context.Rentals.Where(r => r.BookId == bookId && r.UserId == userId && r.ReturnDate == null).ToListAsync();
            var result = new List<Rentals[]>();

            foreach (var rental in rentals)
            {
                result.Add(new Rentals[] { rental });
            }

            return result;
        }

        public Task<bool> CheckDate(string date)
        {
            DateTime today = DateTime.Now.Date;
            string format = "yyyy-MM-dd";

            if (DateTime.TryParseExact(date, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dateTime))
            {
                bool result = dateTime != today;
                return Task.FromResult(result);
            }

            return Task.FromResult(false);
        }

        public async Task<bool?> CheckForecastDate(string forecastDateParam, string rentalDateParam)
        {
            if (DateTime.TryParse(forecastDateParam, out DateTime forecastDate) && DateTime.TryParse(rentalDateParam, out DateTime rentalDate))
            {
                if (forecastDate < rentalDate)
                    return false;

                var diff = forecastDate.Subtract(rentalDate);
                if (diff.Days > 30)
                    return true;
            }
            return null;
        }

        public async Task<bool> GetStatus(string forecastDateParam, string returnDateParam)
        {
            if (DateTime.TryParse(forecastDateParam, out DateTime forecastDate) && DateTime.TryParse(returnDateParam, out DateTime returnDate))
            {
                if (returnDate > forecastDate)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            return true;
        }

    }
}