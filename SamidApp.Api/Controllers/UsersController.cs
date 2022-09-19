using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SamidApp.Data.IRepositories;
using SamidApp.Domain.Configurations;
using SamidApp.Service.Interfaces;

namespace SamidApp.Api.Controllers;

public class UsersController : BaseController
{
    private readonly IUserService userService;
    public UsersController(IUserService userService)
    {
        this.userService = userService;
    }
    
    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllAsync([FromQuery]PaginationParams @params)
        => Ok(await userService.GetAllAsync(@params));
}