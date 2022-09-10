using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Entities.Commons;
using SamidApp.Domain.Entities.Products;

namespace SamidApp.Data.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly MarketDbContext _dbContext;

    public UnitOfWork(MarketDbContext dbContext)
    {
        _dbContext = dbContext;

        Products = new Repository<Product>(_dbContext);
        ProductCategories = new Repository<ProductCategory>(_dbContext);
        Attachments = new Repository<Attachment>(_dbContext);
    }
    
    /// <summary>
    /// Dbsets of database
    /// </summary>
    public IRepository<Product> Products { get; }
    public IRepository<ProductCategory> ProductCategories { get; }
    public IRepository<Attachment> Attachments { get; }

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