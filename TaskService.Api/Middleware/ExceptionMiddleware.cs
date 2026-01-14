using MongoDB.Driver;
using System.Net;
using System.Text.Json;

namespace TaskService.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (MongoConnectionException ex)
        {
            _logger.LogError(ex, "MongoDB connection error");

            await WriteError(context,
                HttpStatusCode.ServiceUnavailable,
                "Database unavailable");
        }
        catch (TimeoutException ex)
        {
            _logger.LogError(ex, "Timeout error");

            await WriteError(context,
                HttpStatusCode.RequestTimeout,
                "Request timeout while accessing database");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unhandled exception");

            await WriteError(context,
                HttpStatusCode.InternalServerError,
                "Unexpected error occurred");
        }
    }

    private static async Task WriteError(
        HttpContext context,
        HttpStatusCode status,
        string message)
    {
        context.Response.StatusCode = (int)status;
        context.Response.ContentType = "application/json";

        var response = new
        {
            status = (int)status,
            error = message
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
