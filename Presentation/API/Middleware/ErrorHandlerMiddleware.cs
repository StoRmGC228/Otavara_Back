namespace API.Middleware;

using System.Net;
using System.Text.Json;
using FluentValidation;

public class ErrorHandlerMiddleware
{
    private readonly ILogger<ErrorHandlerMiddleware> _logger;
    private readonly RequestDelegate _next;

    public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        if (context.Request.Path.StartsWithSegments("/swagger"))
        {
            await _next(context);
            return;
        }

        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, "Unhandled exception caught in middleware");

        var response = context.Response;
        response.ContentType = "application/json";

        var statusCode = exception switch
        {
            ValidationException => HttpStatusCode.BadRequest,
            UnauthorizedAccessException => HttpStatusCode.Unauthorized,
            KeyNotFoundException => HttpStatusCode.NotFound,
            _ => HttpStatusCode.InternalServerError
        };

        response.StatusCode = (int)statusCode;

        var result = statusCode switch
        {
            HttpStatusCode.BadRequest when exception is ValidationException validationException =>
                JsonSerializer.Serialize(new
                {
                    message = "Validation failed",
                    errors = validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })
                }),

            _ => JsonSerializer.Serialize(new
            {
                message = "An error occurred while processing your request"
            })
        };

        await response.WriteAsync(result);
    }
}