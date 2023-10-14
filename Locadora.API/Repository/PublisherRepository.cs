using Microsoft.EntityFrameworkCore;
using Locadora.API.Context;
using Locadora.API.Pagination;
using Locadora.API.Models;
using Locadora.API.Interfaces.IRepository;

namespace Locadora.API.Repository
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
                publishers = publishers.Where(
                    p => p.Id.ToString().Contains(request.FilterValue) ||
                    p.Name.Contains(request.FilterValue) ||
                    p.City.Contains(request.FilterValue)
                );

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
            return await _context.Publishers.Where(p => p.Name == publisherName).ToListAsync();
        }
    }
}