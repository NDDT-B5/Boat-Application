using BoatApi.Data;
using BoatApi.Models;
using BoatApi.Services.Interfaces;

namespace BoatApi.Infrastructure.Seeder;

/// <summary>
/// Seeding default users into the database.
/// </summary>
internal static class UserSeeder
{
    /// <summary>
    /// Ensures the database is seeded with default users if none exist.
    /// </summary>
    /// <param name="app">The WebApplication instance used for resolving dependencies.</param>
    public static void EnsureSeeded(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var passwordService = scope.ServiceProvider.GetRequiredService<IPasswordService>();

        if (context.Users.Any()) return;
        context.Users.AddRange(
            new User("admin", "admin@torchalla.eu", passwordService.HashPassword("admin123"), "Admin"),
            new User("nico", "nico@torchalla.eu", passwordService.HashPassword("nico"), "User"),
            new User("test", "test@torchalla.eu", passwordService.HashPassword("test"), "User")
        );
        context.SaveChanges();
    }
}