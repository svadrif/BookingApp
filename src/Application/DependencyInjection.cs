using Application.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AppUserProfile));
            services.AddAutoMapper(typeof(BookingProfile));
            services.AddAutoMapper(typeof(MapProfile));
            services.AddAutoMapper(typeof(OfficeProfile));
            services.AddAutoMapper(typeof(ParkingPlaceProfile));
            services.AddAutoMapper(typeof(VacationProfile));
            services.AddAutoMapper(typeof(WorkPlaceProfile));
        }
    }
}