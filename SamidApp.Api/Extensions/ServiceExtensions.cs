using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using SamidApp.Data.IRepositories;
using SamidApp.Data.Repositories;
using SamidApp.Service.Interfaces;
using SamidApp.Service.Services;

namespace SamidApp.Api.Extensions;

public static class ServiceExtensions
{
    public static void AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAttachmentService, AttachmentService>();
        services.AddScoped<IUserService, UserService>();
    }

    public static void AddJwtService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(p =>
        {
            var key = Encoding.UTF8.GetBytes(configuration["JWT:Key"]);
            p.SaveToken = true;
            p.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };
        });

        services.AddScoped<IAuthService, AuthService>();
    }
}