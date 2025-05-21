// Ignore Spelling: Middleware

using Horizon.Application.Abstractions.Common;
using System.Net;
using System.Text.Json;

namespace HorizonChain.Web.Common.Middlewares;
public class ExceptionHandlingMiddleware(RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger,
    IHostEnvironment env)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
    private readonly IHostEnvironment _env = env;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            if (!ShouldHandleException(ex))
            {
                throw; // Rethrow exceptions we don't want to handle
            }

            var traceId = context.TraceIdentifier;

            // Enhanced structured logging
            _logger.LogError(ex,
                "Unhandled exception occurred for {Method} {Path} (Trace ID: {TraceId})",
                context.Request.Method,
                context.Request.Path,
                traceId);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new BaseResponse<object>
            {
                Success = false,
                Data = null,
                Error = _env.IsDevelopment()
                    ? $"Unhandled: {ex.Message}"
                    : "An unexpected error occurred.",
                Code = 500
            };

            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
            var json = JsonSerializer.Serialize(response, options);

            await context.Response.WriteAsync(json);
        }
    }

    private bool ShouldHandleException(Exception ex)
    {
        // Skip handling for certain exception types
        // With Result Pattern, we might still want to ignore things like OperationCanceledException
        return !(ex is OperationCanceledException);
    }
}

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
