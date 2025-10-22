using System.Linq.Expressions;
using DevIO.Business.Entities.Base;

namespace DevIO.Business.Interfaces.Repositories.Base;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task AddAsync(TEntity entity);

    Task<TEntity> GetByIdAsync(Guid id);

    Task<List<TEntity>> GetAllAsync();

    Task UpdateAsync(TEntity entity);

    Task RemoveAsync(Guid id);

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task<int> SaveChangesAsync();
}