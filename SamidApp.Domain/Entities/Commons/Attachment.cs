using SamidApp.Domain.Commons;

namespace SamidApp.Domain.Entities.Commons;

public class Attachment : Auditable
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