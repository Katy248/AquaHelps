using AquaHelps.Domain;
using AquaHelps.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Infrastructure.Repositories;
public class Repository<TEntity> : RepositoryBase<TEntity>, IRepository<TEntity> where TEntity : DbEntity
{
    public Repository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task DeleteById(string id, CancellationToken cancellationToken = default)
    {
        var entityToDelete = await GetById(id, cancellationToken);
        if (entityToDelete != null)
            _context.Remove(entityToDelete);

        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<TEntity?> GetById(string id, CancellationToken cancellationToken = default)
    {
        var collection = GetQuery();

        foreach (var statement in _statementsForSingleEntity)
        {
            collection = statement.Invoke(collection);
        }

        return await collection
            .FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
    }
}
