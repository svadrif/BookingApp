using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<State> States { get; set; }
        DbSet<AppUser> AppUsers { get; set; }
        DbSet<Booking> Bookings { get; set; }
        DbSet<Map> Maps { get; set; }
        DbSet<Office> Offices { get; set; }
        DbSet<ParkingPlace> ParkingPlaces { get; set; }
        DbSet<Vacation> Vacations { get; set; }
        DbSet<WorkPlace> WorkPlaces { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
