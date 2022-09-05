using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;

namespace SamidApp.Data.Repositories;

#pragma warning disable
public abstract class Repository<TSource> : IRepository<TSource> where TSource : class
{
    protected readonly MarketDbContext _dbContext;
    protected readonly DbSet<TSource> _dbSet;

    public Repository(MarketDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TSource>();
    }
    
    public IQueryable<TSource> GetAll(Expression<Func<TSource, bool>> expression = null, string include = null, bool isTracking = true)
    {
        IQueryable<TSource> query = expression is null ? _dbSet : _dbSet.Where(expression);

        if (!string.IsNullOrEmpty(include))
            query = query.Include(include);

        if (!isTracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<TSource> AddAsync(TSource entity)
    {
        var entry = await _dbSet.AddAsync(entity);

        return entry.Entity;
    }

    public async Task<TSource> GetAsync(Expression<Func<TSource, bool>> expression = null, string include = null)
    {
        return await GetAll(expression, include).FirstOrDefaultAsync();
    }

    public async Task<TSource> UpdateAsync(TSource entity)
    {
        return _dbSet.Update(entity).Entity;
    }

    public async Task DeleteAsync(Expression<Func<TSource, bool>> expression)
    {
        var entity = await GetAsync(expression);
        
        _dbSet.Remove(entity);
    }
}