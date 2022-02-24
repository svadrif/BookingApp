using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Map> Maps { get; set; }
        public DbSet<Office> Office { get; set; }
        public DbSet<ParkingPlace> ParkingPlaces { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }
    }
}
