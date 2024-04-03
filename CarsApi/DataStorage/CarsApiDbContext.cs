using CarsApi.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsApi.DataStorage
    {
    public class CarsApiDbContext:DbContext
        {
        public DbSet<Car> Cars { get; set; }
        public CarsApiDbContext(DbContextOptions<CarsApiDbContext> options):base (options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);
            }
        }
    }
