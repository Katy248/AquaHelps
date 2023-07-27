using AquaHelps.Domain;

namespace AquaHelps.Infrastructure;
public interface ISearchableRepository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
{
    public IQueryable<TEntity> Search(string searchQuery);
}
