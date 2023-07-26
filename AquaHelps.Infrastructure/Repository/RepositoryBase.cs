using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AquaHelps.Infrastructure.Repository;
public class RepositoryBase<TEntity> where TEntity : class
{
    protected readonly ApplicationDbContext _context;
    protected readonly List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> _statementsForCollection = new();
    protected readonly List<Func<IQueryable<TEntity>, IQueryable<TEntity>>> _statementsForSingleEntity = new();

    public RepositoryBase(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task Create(TEntity entity, CancellationToken cancellationToken = default)
    {
        await _context.AddAsync(entity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public IQueryable<TEntity> GetAll()
    {
        var collection = GetQuery();

        return collection;
    }
    public async Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync(cancellationToken);
        return entity;
    }
    protected IQueryable<TEntity> GetQuery()
    {
        var collection = _context
            .Set<TEntity>()
            .AsQueryable()
            .AsNoTracking();

        foreach (var statement in _statementsForCollection)
        {
            collection = statement.Invoke(collection);
        }

        return collection;
    }
    protected void AddStatement(Func<IQueryable<TEntity>, IQueryable<TEntity>> func, bool onlyForSingleEntity = true)
    {
        if (onlyForSingleEntity)
            _statementsForSingleEntity.Add(func);
        else
            _statementsForCollection.Add(func);
    }
    protected void AddIncludeStatement<TProperty>(Expression<Func<TEntity, TProperty>> func, bool onlyForSingleEntity = true)
    {
        AddStatement(collection => collection.Include(func), onlyForSingleEntity);
    }
}
