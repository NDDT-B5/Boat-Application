using BoatApi.Data;
using BoatApi.Models;
using System.Security.Cryptography;
using System.Text;

namespace BoatApi.Seeder;
internal static class UserSeeder
{
    public static void EnsureSeeded(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Users.Any()) return;
        context.Users.AddRange(
            new User("admin", "admin@torchalla.eu", HashPassword("admin123"), "Admin"),
            new User("nico", "nico@torchalla.eu", HashPassword("nico"), "User"),
            new User("test", "test@torchalla.eu", HashPassword("test"), "User")
        );
        context.SaveChanges();
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hashBytes);
    }
}
