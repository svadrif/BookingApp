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
        public DbSet<Office> Offices { get; set; }
        public DbSet<ParkingPlace> ParkingPlaces { get; set; }
        public DbSet<Vacation> Vacations { get; set; }
        public DbSet<WorkPlace> WorkPlaces { get; set; }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (Microsoft.EntityFrameworkCore.ChangeTracking.EntityEntry<AuditableEntity> entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = Guid.Empty;
                        entry.Entity.CreatedDate = DateTimeOffset.Now;
                        break;

                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = Guid.Empty;
                        entry.Entity.ModifiedDate = DateTimeOffset.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
