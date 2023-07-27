using AquaHelps.Domain;
using AquaHelps.Infrastructure.Repositories;

namespace AquaHelps.Infrastructure.Repository;
public abstract class SearchableRepository<TEntity> : Repository<TEntity>, ISearchableRepository<TEntity> where TEntity : DbEntity
{
    public SearchableRepository(ApplicationDbContext context) : base(context)
    {
    }

    public IQueryable<TEntity> Search(string searchQuery)
    {
        return GetQuery().Where(entity => ContainsSearchQuery(entity, searchQuery));
    }
    private readonly List<Func<TEntity, string>> _searchSelectors = new();
    protected void AddSearchSelector(Func<TEntity, string> selector)
    {
        _searchSelectors.Add(selector);
    }
    private bool ContainsSearchQuery(TEntity entity, string searchQuery)
    {
        foreach (var selector in _searchSelectors)
        {
            if (selector(entity).Contains(searchQuery))
                return true;
        }
        return false;
    }
}
