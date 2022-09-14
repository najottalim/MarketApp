using SamidApp.Domain.Entities.Products;
using SamidApp.Service.Exceptions;

namespace SamidApp.Api.Middlewares;

public class MarketExceptionMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<MarketExceptionMiddleware> logger;
    public MarketExceptionMiddleware(RequestDelegate next, ILogger<MarketExceptionMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next.Invoke(context);
        }
        catch (MarketException ex)
        {
            await HandleExceptionAsync(context, ex.Code, ex.Message);
        }
        catch (Exception ex)
        {
            // log
            logger.LogError(ex.ToString());
            
            await HandleExceptionAsync(context, 500, ex.Message);
        }
    }
    /// <summary>
    /// Input: 121 (True)
    /// Input: 122.1 (False)
    /// Input: 122.221 (True)
    /// Don't use string class and generic collections
    /// </summary>
    /// <param name="context"></param>
    /// <param name="code"></param>
    /// <param name="message"></param>
    public async Task HandleExceptionAsync(HttpContext context, int code, string message)
    {
        context.Response.StatusCode = code;
        await context.Response.WriteAsJsonAsync(new
        {
            Code = code,
            Message = message
        });
    }
}