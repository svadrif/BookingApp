using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Map> Maps { get; set; }
        DbSet<Office> Office { get; set; }
        DbSet<ParkingPlace> ParkingPlaces { get; set; }
        DbSet<Vacation> Vacations { get; set; }
        DbSet<WorkPlace> WorkPlaces { get; set; }

        Task<int> SaveChanges();
    }
}
