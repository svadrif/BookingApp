using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
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

            services.AddScoped(typeof(IVacationService), typeof(VacationService));
            services.AddScoped(typeof(IAppUserService), typeof(AppUserService));
            services.AddScoped(typeof(IBookingService), typeof(BookingService));
            services.AddScoped(typeof(IWorkPlaceService), typeof(WorkPlaceService));
            services.AddScoped(typeof(IMapService), typeof(MapService));
            services.AddScoped(typeof(IOfficeService), typeof(OfficeService));
            services.AddScoped(typeof(IParkingPlaceService), typeof(ParkingPlaceService));
        }
    }
}
