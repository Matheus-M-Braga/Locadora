using Locadora.API.Models;
using Locadora.API.Data;
using Microsoft.EntityFrameworkCore;
using Locadora.API.Helpers;
using Locadora.API.Repository.Pagination;
using Locadora.API.FiltersDb;
using Locadora.API.Dtos;

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

        public async Task<List<Publishers>> GetAllPublishers()
        {
            return await _context.Publishers.ToListAsync();
        }

        public async Task<Publishers> GetPublisherById(int publisherId)
        {
            return await _context.Publishers.FirstOrDefaultAsync(p => p.Id == publisherId);
        }

        public async Task<List<Publishers>> GetPublisherByName(string publisherName)
        {
            return await _context.Publishers.Where(p => p.Name == publisherName).ToListAsync();
        }

        public async Task<PagedBaseResponse<Publishers>> GetPaged(PublisherFilterDb request) {
            var publisher = _context.Publishers.AsQueryable();
            if (!string.IsNullOrEmpty(request.Name))
                publisher = publisher.Where(p => p.Name.Contains(request.Name));

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Publishers>, Publishers>(publisher, request);
        }

        
    }
}