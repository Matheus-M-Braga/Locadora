using Locadora.API.Models;
using Locadora.API.Context;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Locadora.API.Repository.Pagination;

namespace Locadora.API.Repository
{
    public class RentalRepository : IRentalRepository
    {
        private readonly DataContext _context;
        public RentalRepository(DataContext context)
        {
            _context = context;
        }

        public async Task Add(Rentals entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Rentals entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Delete(Rentals entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedBaseResponse<Rentals>> GetAllRentals(FilterDb request)
        {
            var rentals = _context.Rentals.Include(r => r.User).Include(r => r.Book).AsQueryable();
            if (request.FilterValue != null)
                rentals = rentals.Where(
                    r => r.Id.ToString().Contains(request.FilterValue) ||
                    r.RentalDate.Contains(request.FilterValue) ||
                    r.ForecastDate.Contains(request.FilterValue) ||
                    r.ReturnDate.Contains(request.FilterValue) ||
                    r.BookId.ToString().Contains(request.FilterValue) ||
                    r.Book.Name.ToString().Contains(request.FilterValue) ||
                    r.UserId.ToString().Contains(request.FilterValue) ||
                    r.User.Name.Contains(request.FilterValue)
                );

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Rentals>, Rentals>(rentals, request);
        }

        public async Task<Rentals> GetRentalById(int rentalId)
        {
            return await _context.Rentals.AsNoTracking().Include(r => r.User).Include(r => r.Book).FirstOrDefaultAsync(r => r.Id == rentalId);
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