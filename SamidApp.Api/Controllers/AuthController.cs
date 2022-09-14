using Microsoft.AspNetCore.Mvc;
using SamidApp.Service.DTOs.Users;
using SamidApp.Service.Interfaces;

namespace SamidApp.Api.Controllers;

public class AuthController : BaseController
{
    private readonly IAuthService authService;
    public AuthController(IAuthService authService)
    {
        this.authService = authService;
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login(UserForLoginDto dto)
    {
        var token = await authService.GenerateTokenAsync(dto.Login, dto.Password);
        
        return Ok(new
        {
            Token = token
        });
    }
}