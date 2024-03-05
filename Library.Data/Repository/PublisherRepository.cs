using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Business.Pagination;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Library.Data.Repository
{
    public class PublisherRepository : Repository<Publishers>, IPublisherRepository
    {
        public PublisherRepository(DataContext context) : base(context)
        {
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