using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Entities.Products;

namespace SamidApp.Data.Repositories;

public class ProductCategoryRepository : Repository<ProductCategory>, IProductCategoryRepository
{
    public ProductCategoryRepository(MarketDbContext dbContext) : base(dbContext)
    {
    }
}