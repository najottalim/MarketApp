using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SamidApp.Api.Extensions;
using SamidApp.Api.Middlewares;
using SamidApp.Data.DbContexts;
using SamidApp.Data.IRepositories;
using SamidApp.Data.Repositories;
using SamidApp.Service.Helpers;
using SamidApp.Service.Interfaces;
using SamidApp.Service.Mappers;
using SamidApp.Service.Services;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(options => 
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

builder.Services.AddDbContext<MarketDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketDb")));
builder.Services.AddAutoMapper(typeof(MapperProfile));

// Jwt
builder.Services.AddJwtService(builder.Configuration);

// Custom services
builder.Services.AddCustomServices();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Serilog
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

EnvironmentHelper.WebRootPath = app.Services.GetService<IWebHostEnvironment>()?.WebRootPath;

app.UseMiddleware<MarketExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();