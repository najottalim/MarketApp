namespace SamidApp.Service.Interfaces;

public interface IAuthService
{ 
    Task<string> GenerateTokenAsync(string login, string password);
}