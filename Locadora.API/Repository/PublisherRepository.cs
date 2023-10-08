using Locadora.API.Models;
using Locadora.API.Context;
using Microsoft.EntityFrameworkCore;
using Locadora.API.Repository.Pagination;
using Locadora.API.FiltersDb;

namespace Locadora.API.Repository
{
    public class PublisherRepository : IPublisherRepository
    {
        private readonly DataContext _context;
        public PublisherRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<Publishers> Add(Publishers entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
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
                publishers = FilterHelper.ApplyFilter(publishers, request.FilterValue);

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