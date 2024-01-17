using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Products.Domain.Entities;
using Products.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.Infrastructure.Database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        public DbSet<Food> Foods { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Clothes> Clothes { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
    }
}
