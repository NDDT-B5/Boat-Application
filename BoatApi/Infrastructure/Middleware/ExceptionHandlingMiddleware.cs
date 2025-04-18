namespace BoatApi.Infrastructure.Middleware;

internal sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (KeyNotFoundException ex)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            await httpContext.Response.WriteAsync($"Resource not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle other exceptions if needed
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            await httpContext.Response.WriteAsync($"Internal Server Error: {ex.Message}");
        }
    }
}