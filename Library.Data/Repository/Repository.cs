using Library.Business.Interfaces.IRepository;
using Library.Business.Models;
using Library.Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Library.Data.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly DataContext _context;
        protected readonly DbSet<TEntity> _contextSet;

        public Repository(DataContext context)
        {
            _context = context;
            _contextSet = context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            _contextSet.Add(entity);
            await SaveChanges();
        }

        public async Task Update(TEntity entity)
        {
            _contextSet.Update(entity);
            await SaveChanges();
        }

        public async Task Delete(TEntity entity)
        {
            _contextSet.Remove(entity);
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
        {
            return await _contextSet.AsNoTracking().Where(predicate).ToListAsync();
        }
    }
}
