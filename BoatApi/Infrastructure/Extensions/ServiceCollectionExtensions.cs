using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BoatApi.Data;
using Microsoft.EntityFrameworkCore;
using BoatApi.Infrastructure.DependencyInjection;

namespace BoatApi.Infrastructure.Extensions;

/// <summary>
/// Contains extension methods for registering services in the dependency injection container.
/// </summary>
internal static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds JWT authentication configuration to the service collection.
    /// </summary>
    /// <param name="services">The service collection to which the authentication is added.</param>
    /// <param name="config">The application configuration used to retrieve JWT settings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration config)
    {
        var jwtKey = config["Jwt:Key"];
        var jwtIssuer = config["Jwt:Issuer"];

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtIssuer,
                ValidAudience = jwtIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
            };
        });

        return services;
    }

    /// <summary>
    /// Adds Boat API-specific services to the service collection and the DbContext and the AutoMapper.
    /// </summary>
    /// <param name="services">The service collection to which services are added.</param>
    /// <param name="config">The application configuration used for setting up database connection strings.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddBoatApiServices(this IServiceCollection services, IConfiguration config)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(config.GetConnectionString("DefaultConnection")));

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddBoatApiScopedServices();
        return services;
    }

    /// <summary>
    /// Configures and adds a CORS policy that allows authenticated and non-authenticated requests from the Angular development server.
    /// </summary>
    /// <param name="services">The service collection to which the CORS policy is added.</param>
    /// <returns>The updated service collection.</returns>
    public static IServiceCollection AddCorsForFrontend(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAngularDev", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyMethod()
                      .AllowAnyHeader()
                      .AllowCredentials();
            });
        });

        return services;
    }
}