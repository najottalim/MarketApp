using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;
using SamidApp.Data.Repositories;
using SamidApp.Service.Interfaces;
using SamidApp.Service.Mappers;
using SamidApp.Service.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => 
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<MarketDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketDb")));
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Custom services
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

builder.Services.AddScoped<IProductService, ProductService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();