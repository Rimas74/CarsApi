using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarsApi.DataStorage
    {
    public class CarAPIAuthDbContext : IdentityDbContext
        {
        public CarAPIAuthDbContext(DbContextOptions<CarAPIAuthDbContext> options) : base(options)
            {
            }

        protected override void OnModelCreating(ModelBuilder builder)
            {
            base.OnModelCreating(builder);

            var readerRoleId = "09631ee0-e47e-4bc9-aaed-842fd537b4e4";
            var writerRoleId = "3910cdce-8a93-4190-99aa-0fea6989b4bc";


            var roles = new List<IdentityRole>
                {
                new IdentityRole
                    {
                    Id=readerRoleId,
                    ConcurrencyStamp=readerRoleId,
                    Name="Reader",
                    NormalizedName="Reader".ToUpper(),
                    },
                new IdentityRole
                    {
                    Id=writerRoleId,
                    ConcurrencyStamp=writerRoleId,
                    Name="Writer",
                    NormalizedName="Writer".ToUpper(),
                    }
                };
            builder.Entity<IdentityRole>().HasData(roles);
            }
        }
    }
