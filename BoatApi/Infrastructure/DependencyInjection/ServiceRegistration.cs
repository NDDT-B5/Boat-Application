using BoatApi.Services;
using BoatApi.Services.Interfaces;

namespace BoatApi.Infrastructure.DependencyInjection;

/// <summary>
/// Contains extension methods for registering BoatAPI services.
/// </summary>
internal static class ServiceRegistration
{
    /// <summary>
    /// Adds scoped service registrations used throughout the Boat API application.
    /// </summary>
    /// <param name="services">The service collection to add services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/> with service registrations.</returns>
    public static IServiceCollection AddBoatApiScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IBoatService, BoatService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IPasswordService, PasswordService>();

        return services;
    }
}