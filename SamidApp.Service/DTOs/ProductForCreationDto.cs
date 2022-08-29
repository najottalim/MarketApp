namespace SamidApp.Service.DTOs;

public class ProductForCreationDto
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public long CategoryId { get; set; }
}