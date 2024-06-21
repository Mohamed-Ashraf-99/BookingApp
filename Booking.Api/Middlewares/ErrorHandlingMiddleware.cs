using Booking.Domain.Exceptions;

namespace Booking.Api.Middlewares;

public class ErrorHandlingMiddleware(ILogger<ErrorHandlingMiddleware> _logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (NotFoundException notFound)
        {
            _logger.LogWarning(notFound, "Resource not found.");

            context.Response.StatusCode = 404;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = 404,
                Message = notFound.Message
            };

            await context.Response.WriteAsJsonAsync(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unhandled exception has occurred while executing the request.");

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = 500,
                Message = "Internal Server Error. Please try again later.",
                Detailed = context.RequestServices.GetService<IWebHostEnvironment>()?.IsDevelopment() == true ? ex.ToString() : null
            };

            await context.Response.WriteAsJsonAsync(response);
        }
    }
}
