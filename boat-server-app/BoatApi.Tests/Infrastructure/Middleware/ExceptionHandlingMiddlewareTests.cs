using System.Net;
using BoatApi.Infrastructure.Middleware;
using Microsoft.AspNetCore.Http;

namespace BoatApi.Tests.Infrastructure.Middleware;

public class ExceptionHandlingMiddlewareTests
{
    [Fact]
    public async Task InvokeAsync_WhenKeyNotFoundExceptionThrown_Returns404()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var middleware = new ExceptionHandlingMiddleware(_ => throw new KeyNotFoundException("Boat not found"));

        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.Equal((int)HttpStatusCode.NotFound, context.Response.StatusCode);
        Assert.Contains("Resource not found", body);
        Assert.Contains("Boat not found", body);
    }

    [Fact]
    public async Task InvokeAsync_WhenExceptionThrown_Returns500()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var middleware = new ExceptionHandlingMiddleware(_ => throw new Exception("Error thrown"));

        var responseStream = new MemoryStream();
        context.Response.Body = responseStream;

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        context.Response.Body.Seek(0, SeekOrigin.Begin);
        var body = await new StreamReader(context.Response.Body).ReadToEndAsync();

        Assert.Equal((int)HttpStatusCode.InternalServerError, context.Response.StatusCode);
        Assert.Contains("Error thrown", body);
        Assert.Contains("Internal Server Error", body);
    }

    [Fact]
    public async Task InvokeAsync_WhenNoException_PassesThrough()
    {
        // Arrange
        var context = new DefaultHttpContext();
        var middleware = new ExceptionHandlingMiddleware(_ =>
        {
            context.Response.StatusCode = StatusCodes.Status200OK;
            return Task.CompletedTask;
        });

        // Act
        await middleware.InvokeAsync(context);

        // Assert
        Assert.Equal(StatusCodes.Status200OK, context.Response.StatusCode);
    }
}