using CarRent.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CarRent.Data.Persistence
{
    public class CarRentDbContext : DbContext
    {
        public CarRentDbContext(DbContextOptions<CarRentDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
