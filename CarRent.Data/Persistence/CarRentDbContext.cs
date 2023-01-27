using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRent.Data.Persistence
{
    public class CarRentDbContext : DbContext
    {
        public CarRentDbContext()
        {
        }

        public CarRentDbContext(DbContextOptions<CarRentDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=49153;Database=rentcar;User Id=postgres;Password=postgrespw;");
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Booking> Bookings { get; set; }
    }
}
