using SamidApp.Domain.Commons;
using SamidApp.Domain.Enums;

namespace SamidApp.Domain.Entities.Users;

public class User : Auditable
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public UserRole Role { get; set; } = UserRole.User;
    public ItemState State { get; set; } = ItemState.Created;
} 