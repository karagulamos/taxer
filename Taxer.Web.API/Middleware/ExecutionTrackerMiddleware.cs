using System.Diagnostics;

namespace Taxer.Web.API.Middleware;

public class ExecutionTrackerMiddleware(ILogger<ExecutionTrackerMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        // Start timing of the request
        var watch = Stopwatch.StartNew();

        // Continue processing the request using the next middleware in the pipeline
        await next(context);

        // Stop timing of the request
        watch.Stop();

        // Calculate the elapsed time
        var elapsedTime = watch.Elapsed;

        // Log the execution time of the request
        logger.LogInformation(
            "Request: {method} {path} completed in {elapsedTime:hh\\:mm\\:ss\\.fff}",
            context.Request.Method,
            context.Request.Path,
            elapsedTime
        );
    }
}

