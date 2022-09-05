using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;

namespace SamidApp.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MarketDbContext _dbContext;

    public UnitOfWork(MarketDbContext dbContext)
    {
        _dbContext = dbContext;

        Products = new ProductRepository(_dbContext);
        ProductCategories = new ProductCategoryRepository(_dbContext);
    }
    
    /// <summary>
    /// Dbsets of database
    /// </summary>
    public IProductRepository Products { get; }
    public IProductCategoryRepository ProductCategories { get; }
    
    /// <summary>
    /// Dispose object
    /// </summary>
    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Save changes in ORM
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}