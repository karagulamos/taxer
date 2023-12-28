using System.Net;
using System.Text.Json;
using Taxer.Core.Common;

namespace Taxer.Web.API.Middleware;

public class DefaultExceptionHandlerMiddleware(ILogger<DefaultExceptionHandlerMiddleware> logger) : IMiddleware
{
    private readonly JsonSerializerOptions _serializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "{Message}", ex.Message);

            var jsonResponse = JsonSerializer.Serialize(
                new Error($"{(int)HttpStatusCode.InternalServerError}", "Internal Server Error"),
                _serializerOptions
            );

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            await context.Response.WriteAsync(jsonResponse);
        }
    }
}
