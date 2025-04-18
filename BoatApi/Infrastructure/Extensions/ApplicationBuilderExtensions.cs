namespace BoatApi.Infrastructure.Extensions;

/// <summary>
/// Provides extension methods for configuring the HTTP request pipeline in the Boat API application.
/// </summary>
internal static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Adds and configures OpenAPI (Swagger) middleware.
    /// Currently only if the application is running in a development environment.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    public static void UseOpenApi(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment()) return;

        app.MapOpenApi();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/openapi/BoatAPI.json", "BoatAPI");
            options.RoutePrefix = "swagger";
        });
    }

    /// <summary>
    /// Enables the configured CORS policy.
    /// Currently only if the application is running in a development environment.
    /// </summary>
    /// <param name="app">The <see cref="WebApplication"/> instance to configure.</param>
    public static void UseCorsPolicy(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.UseCors("AllowAngularDev");
        }
    }
}