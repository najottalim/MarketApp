using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Entities.Products;

namespace SamidApp.Data.Repositories;

public class ProductRepository : Repository<Product>, IProductRepository
{
    public ProductRepository(MarketDbContext dbContext) : base(dbContext)
    {
    }
}