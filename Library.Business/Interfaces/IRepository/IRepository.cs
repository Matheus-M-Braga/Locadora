using Library.Business.Models;
using System.Linq.Expressions;

namespace Library.Business.Interfaces.IRepository
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(int id);
        Task<int> SaveChanges();
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
    }
}
