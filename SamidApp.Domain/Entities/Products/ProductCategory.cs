using SamidApp.Domain.Commons;

namespace SamidApp.Domain.Entities.Products;

public class ProductCategory : Auditable<long>
{
    public string Name { get; set; }
    public virtual ICollection<Product> Products { get; set; }
}