namespace BoatApi.Infrastructure.Seeder;

/// <summary>
/// A class responsible for seeding the database with initial data, including default users and boats.
/// </summary>
public static class AppSeeder
{
    /// <summary>
    /// Ensures the database is seeded with default users and boats if none exist.
    /// </summary>
    /// <param name="app">The WebApplication instance used for resolving dependencies.</param>
    public static void EnsureSeeded(WebApplication app)
    {
        UserSeeder.EnsureSeeded(app);
        BoatSeeder.EnsureSeeded(app);
    }
}