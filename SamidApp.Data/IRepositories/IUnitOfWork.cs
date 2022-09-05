namespace SamidApp.Data.IRepositories;

public interface IUnitOfWork : IDisposable
{
    IProductRepository Products { get; }
    IProductCategoryRepository ProductCategories { get; }

    Task SaveChangesAsync();
}