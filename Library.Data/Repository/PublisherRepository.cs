using Microsoft.EntityFrameworkCore;
using Library.Data.Context;
using Library.Business.Pagination;
using Library.Business.Models;
using Library.Business.Interfaces.IRepository;
using System.Globalization;

namespace Library.Data.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataContext _context;
        public PublisherRepository(DataContext context)
        {
            _context = context;
        }
        public async Task Add(Publishers entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Publishers entity)
        {
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Publishers entity)
        {
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<PagedBaseResponse<Publishers>> GetAllPublishersPaged(FilterDb request)
        {
            var publishers = _context.Publishers.AsQueryable();
            if (request.FilterValue != null)
            {
                var search = request.FilterValue.ToLower();
                publishers = publishers.Where(
                    p => p.Id.ToString().Contains(search) ||
                    p.Name.ToLower().Contains(search) ||
                    p.City.ToLower().Contains(search)
                );
            }
            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Publishers>, Publishers>(publishers, request);
        }

        public async Task<List<Publishers>> GetAllPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publishers> GetPublisherById(int publisherId)
        {
            return await _context.Publishers.AsNoTracking().FirstOrDefaultAsync(p => p.Id == publisherId);
        }

        public async Task<List<Publishers>> GetPublisherByName(string publisherName)
        {
            return await _context.Publishers.AsNoTracking().Where(p => p.Name == publisherName).ToListAsync();
        }
    }
}