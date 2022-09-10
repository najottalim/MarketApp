using SamidApp.Domain.Entities.Commons;
using SamidApp.Domain.Entities.Products;

namespace SamidApp.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Product> Products { get; }
    IRepository<ProductCategory> ProductCategories { get; }
    IRepository<Attachment> Attachments { get; }

    Task SaveChangesAsync();
}