using AquaHelps.Domain;

namespace AquaHelps.Infrastructure;
public interface IRepository<TEntity> where TEntity : DbEntity
{
    Task<TEntity?> GetById(string id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetAll();
    Task Create(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default);
    Task DeleteById(string id, CancellationToken cancellationToken = default);
}
