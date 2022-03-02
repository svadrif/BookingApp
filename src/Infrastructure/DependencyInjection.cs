using Application.Interfaces;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region Repositories            
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IMapRepository, MapRepository>();
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IOfficeRepository, OfficeRepository>();
            services.AddScoped<IParkingPlaceRepository, ParkingPlaceRepository>();
            services.AddScoped<IVacationRepository, VacationRepository>();
            services.AddScoped<IWorkPlaceRepository, WorkPlaceRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            #endregion

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection"),
                    b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));

            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
        }
    }
}
