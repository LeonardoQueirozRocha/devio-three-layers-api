using System.Linq.Expressions;
using DevIO.Business.Entities.Base;

namespace DevIO.Business.Interfaces.Repositories.Base;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<List<TEntity>> GetAllAsync();

    Task<TEntity?> GetByIdAsync(Guid id);

    Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);

    Task AddAsync(TEntity entity);

    Task UpdateAsync(TEntity entity);

    Task RemoveAsync(Guid id);

    Task<int> SaveChangesAsync();
}