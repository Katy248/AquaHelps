using System.Linq.Expressions;
using System.Collections.Generic;
using System.Linq;
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
        var query = GetQuery();

        query = SearchExpression(query, searchQuery);
        

        return query;
    }
    
    protected Func<IQueryable<TEntity>, string, IQueryable<TEntity>> SearchExpression { get; set; }
}
