using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Infrastructure.Database
{
    public class RestaurantDbContext : DbContext
    {
        public IConfiguration Configuration { get; private set; }
        private readonly Assembly _dbContextAssembly;

        public RestaurantDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
            _dbContextAssembly = this.GetType().Assembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DatabaseConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(_dbContextAssembly);
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Table> Tables { get; set; }

        public DbSet<Review> Reviews { get; set; }

        public DbSet<Restaurant> Restaurants { get; set; }

        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<UserCredentials> UserCredentials { get; set; }
    }
}