using SamidApp.Domain.Commons;
using SamidApp.Domain.Entities.Commons;

namespace SamidApp.Domain.Entities.Products;

public class Product : Auditable
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    
    public long CategoryId { get; set; }
    public ProductCategory Category { get; set; }
    
    public long? FileId { get; set; }
    public Attachment File { get; set; } 
}