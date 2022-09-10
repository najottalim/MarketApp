using SamidApp.Domain.Commons;

namespace SamidApp.Domain.Entities.Commons;

public class Attachment : Auditable<long>
{
    /// <summary>
    /// botirali.png
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// images/botirali.png
    /// </summary>
    public string Path { get; set; }
}