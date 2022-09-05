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
    }
}