using Microsoft.EntityFrameworkCore;
using SamidApp.Domain.Entities.Commons;
using SamidApp.Domain.Entities.Products;
using SamidApp.Domain.Entities.Users;

namespace SamidApp.Data.DbContexts;

public class MarketDbContext : DbContext
{
    public MarketDbContext(DbContextOptions<MarketDbContext> options)
        : base(options)
    {
    }
    
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ProductCategory> ProductCategories { get; set; }
    public virtual DbSet<Attachment> Attachments { get; set; }
    public virtual DbSet<User> Users { get; set; }
}