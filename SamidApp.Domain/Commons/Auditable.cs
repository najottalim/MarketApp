namespace SamidApp.Domain.Commons;

public class Auditable<T> 
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}