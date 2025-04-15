namespace BoatApi.Data;

using Microsoft.EntityFrameworkCore;
using BoatApi.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<Boat> Boats { get; set; }
}