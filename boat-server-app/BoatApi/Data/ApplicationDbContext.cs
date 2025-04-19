using BoatApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BoatApi.Data;

/// <summary>
/// Represents the database context for the Boat API application.
/// Inherits from <see cref="DbContext"/> to interact with the database.
/// This class manages the connection to the database and exposes DbSet properties
/// that allow querying and persisting data for different models in the application.
/// </summary>
internal sealed class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets or sets the collection of <see cref="Boat"/> entities in the database.
    /// This property is used to perform CRUD operations on the Boats table in the database.
    /// </summary>
    public DbSet<Boat> Boats { get; set; }

    public DbSet<User> Users { get; set; }
}