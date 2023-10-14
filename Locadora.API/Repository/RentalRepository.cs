using Microsoft.EntityFrameworkCore;
using System.Globalization;
using Locadora.API.Context;
using Locadora.API.Pagination;
using Locadora.API.Models;
using Locadora.API.Dtos;
using Locadora.API.Interfaces.IRepository;

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
                    r.RentalDate.ToString().Contains(request.FilterValue) ||
                    r.ForecastDate.ToString().Contains(request.FilterValue) ||
                    r.ReturnDate.ToString().Contains(request.FilterValue) ||
                    r.BookId.ToString().Contains(request.FilterValue) ||
                    r.Book.Name.ToString().Contains(request.FilterValue) ||
                    r.UserId.ToString().Contains(request.FilterValue) ||
                    r.User.Name.Contains(request.FilterValue)
                );

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Rentals>, Rentals>(rentals, request);
        }
        public async Task<List<Rentals>> GetAllRentalsDash()
        {
            return await _context.Rentals.Include(r => r.Book).ToListAsync();
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