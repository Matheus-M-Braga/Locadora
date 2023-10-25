using Microsoft.EntityFrameworkCore;
using Library.Data.Context;
using Library.Business.Pagination;
using Library.Business.Models;
using Library.Business.Interfaces.IRepository;

namespace Library.Data.Repository
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

        public async Task<PagedBaseResponse<Rentals>> GetAllRentalsPaged(FilterDb request)
        {
            var rentals = _context.Rentals.Include(r => r.User).Include(r => r.Book).AsQueryable();
            if (request.FilterValue != null)
            {
                var search = request.FilterValue.ToLower();
                rentals = rentals.Where(
                    r => r.Id.ToString().Contains(search) ||
                    r.RentalDate.ToString().Contains(search) ||
                    r.ForecastDate.ToString().Contains(search) ||
                    r.ReturnDate.ToString().Contains(search) ||
                    r.Status.ToLower().Contains(search) ||
                    r.BookId.ToString().Contains(search) ||
                    r.Book.Name.ToLower().Contains(search) ||
                    r.UserId.ToString().Contains(search) ||
                    r.User.Name.ToLower().Contains(search)
                );
            }
            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Rentals>, Rentals>(rentals, request);
        }
        public async Task<List<Rentals>> GetAllRentals()
        {
            return await _context.Rentals.Include(r => r.Book).ToListAsync();
        }

        public async Task<Rentals> GetRentalById(int rentalId)
        {
            return await _context.Rentals.AsNoTracking().Include(r => r.User).Include(r => r.Book).FirstOrDefaultAsync(r => r.Id == rentalId);
        }

        public async Task<List<Rentals>> GetAllRentalsByUserId(int userId)
        {
            return await _context.Rentals.Where(r => r.UserId == userId).ToListAsync();
        }

        public async Task<List<Rentals>> GetAllRentalsByBookId(int bookId)
        {
            return await _context.Rentals.Where(r => r.BookId == bookId).ToListAsync();
        }

        public async Task<List<Rentals>> GetRentalByUserIdandBookId(int bookId, int userId)
        {
            return await _context.Rentals.Where(r => r.BookId == bookId && r.UserId == userId && r.ReturnDate == null).ToListAsync();
        }

        public Task<bool> CheckDate(DateTime date)
        {
            DateTime today = DateTime.Now.Date;

            if (date.Date != today)
            {
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }

        public async Task<bool?> CheckForecastDate(DateTime forecastDate, DateTime rentalDate)
        {

            if (forecastDate < rentalDate)
                return false;

            var diff = forecastDate.Subtract(rentalDate);
            if (diff.Days > 30)
                return true;

            return null;
        }

        public async Task<string> GetStatus(DateTime ForecastDate, DateTime? ReturnDate)
        {
            if (ForecastDate < ReturnDate) return "Atrasado";

            return "No prazo";
        }
    }
}