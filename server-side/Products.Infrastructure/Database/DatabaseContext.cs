using Microsoft.EntityFrameworkCore;
using Products.Domain.Entities;

namespace Products.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Material> Materials { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredient>().HasIndex(e => new { e.Name }).IsUnique();
            modelBuilder.Entity<Food>().HasIndex(e => new { e.Name }).IsUnique();
            modelBuilder.Entity<Device>().HasIndex(e => new { e.Name }).IsUnique();
            modelBuilder.Entity<Clothes>().HasIndex(e => new { e.Name }).IsUnique();
            modelBuilder.Entity<Material>().HasIndex(e => new { e.Name }).IsUnique();
        }
    }
}
