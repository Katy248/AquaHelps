using AquaHelps.Domain;

namespace AquaHelps.Infrastructure.Repository;
public static class RepositoryExtensions
{
    public static async Task<TEntity> GetByIdRequired<TEntity>(this IRepository<TEntity> repository, string id, CancellationToken cancellationToken = default) where TEntity : DbEntity
    {
        var entity = await repository.GetById(id);

        if (entity == null)
            throw new KeyNotFoundException($"There no entity with key \"${id}\" in set.");

        return entity;
    }
}
