using SamidApp.Domain.Entities.Commons;
using SamidApp.Domain.Entities.Products;
using SamidApp.Domain.Entities.Users;

namespace SamidApp.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IRepository<Product> Products { get; }
    IRepository<ProductCategory> ProductCategories { get; }
    IRepository<Attachment> Attachments { get; }
    IRepository<User> Users { get; }

    Task SaveChangesAsync();
}