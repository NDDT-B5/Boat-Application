using BoatApi.Data;
using BoatApi.Models;
using BoatApi.Services.Interfaces;
using System.Linq;

namespace BoatApi.Infrastructure.Seeder;

/// <summary>
/// Seeding default boats into the database.
/// </summary>
public static class BoatSeeder
{
    /// <summary>
    /// Ensures the database is seeded with default boats if none exist.
    /// </summary>
    /// <param name="app">The WebApplication instance used for resolving dependencies.</param>
    public static void EnsureSeeded(WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        if (context.Boats.Any()) return;

        // Add some sample boats
        context.Boats.AddRange(
            new Boat("The Ocean Voyager", "A sturdy vessel built for long expeditions across the open sea, ready to explore new horizons."),
            new Boat("Waves of Serenity", "A tranquil yacht designed for luxurious leisure and peaceful voyages across calm waters."),
            new Boat("Stormbreaker", "A powerful, storm-resistant boat capable of withstanding the fiercest winds and waves."),
            new Boat("Tide Rider", "A sleek, fast boat designed to cut through the waves with precision and speed."),
            new Boat("Seashell Symphony", "An elegant sailboat known for its serene and smooth sailing experience, perfect for a relaxing day on the water."),
            new Boat("The Kraken’s Call", "A majestic vessel with a bold, mythical name, designed for brave adventurers who dare to explore the deep."),
            new Boat("Saltwater Mirage", "A high-tech yacht that blends cutting-edge features with the beauty of the open ocean."),
            new Boat("Wind Whisperer", "A sailboat crafted for those who wish to listen to the gentle wind and drift peacefully across the waves."),
            new Boat("Sunset Wanderer", "A boat designed for watching sunsets from the deck, offering panoramic views and unmatched comfort."),
            new Boat("Aquatic Dreamer", "A luxury yacht with every imaginable amenity, offering its passengers the perfect blend of adventure and relaxation."),
            new Boat("Pirate’s Legacy", "A bold and daring boat with a historic, pirate-inspired design, perfect for treasure hunters and thrill-seekers."),
            new Boat("Blue Horizon", "A majestic cruise ship, offering a premium experience for long-distance travelers who wish to sail into the endless blue."),
            new Boat("The Nomad’s Haven", "A rugged yet elegant boat built for adventurers who prefer to explore secluded islands and undiscovered waters."),
            new Boat("Coral Dream", "A small but beautifully designed boat perfect for leisurely exploration of coral reefs and underwater sights."),
            new Boat("Sirena’s Serenade", "A charming sailboat named after mythical sirens, offering graceful sailing with the melody of the ocean breeze."),
            new Boat("Deepwater Guardian", "A heavy-duty vessel built for deep-sea exploration and protecting the seas, equipped with advanced tech for underwater discovery.")
        );

        context.SaveChanges();
    }
}
