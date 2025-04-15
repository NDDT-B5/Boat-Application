using Microsoft.EntityFrameworkCore;
using BoatApi.Models;

namespace BoatApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Boat> Boats { get; set; }
    }
}