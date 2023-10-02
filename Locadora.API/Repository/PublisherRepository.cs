using Locadora.API.Models;
using Locadora.API.Data;
using Microsoft.EntityFrameworkCore;
using Locadora.API.Helpers;
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
        async Task IPublisherRepository.Add<T>(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }
        async Task IPublisherRepository.Update<T>(T entity)
        {
            _context.Set<T>().Update(entity);
        }
        async Task<bool> IPublisherRepository.SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }
        async Task IPublisherRepository.Delete<T>(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task<List<Publishers>> GetAllPublishers()
        {
            IQueryable<Publishers> query = _context.Publishers;

            query = query.AsNoTracking().OrderBy(publisher => publisher.Id);
            return await query.ToListAsync();
        }

        public async Task<Publishers> GetPublisherById(int publisherId)
        {
            IQueryable<Publishers> query = _context.Publishers;

            query = query.AsNoTracking().OrderBy(publisher => publisher.Id).Where(publisher => publisher.Id == publisherId);
            return await query.FirstOrDefaultAsync();
        }

        public async Task<List<Publishers>> GetPublisherByName(string publisherName)
        {
            return await _context.Publishers.Where(p => p.Name == publisherName).ToListAsync();
        }

        public async Task<PagedBaseResponse<Publishers>> GetPaged(PublisherFilterDb request) {
            var publisher = _context.Publishers.AsQueryable();
            if (string.IsNullOrEmpty(request.Name))
                publisher = publisher.Where(p => p.Name.Contains(request.Name));

            return await PagedBaseResponseHelper.GetResponseAsync<PagedBaseResponse<Publishers>, Publishers>(publisher, request);
        }
    }
}