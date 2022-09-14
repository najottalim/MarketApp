namespace SamidApp.Domain.Commons;

#pragma warning disable
public class Auditable<T> 
{
    public T Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public T? CreatedBy { get; set; }
    public T? UpdatedBy { get; set; }
    public T? DeletedBy { get; set; }
}