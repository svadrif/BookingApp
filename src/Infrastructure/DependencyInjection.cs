using Application.Authentication;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            #region Services
            services.AddScoped(typeof(IVacationService), typeof(VacationService));
            services.AddScoped(typeof(IAppUserService), typeof(AppUserService));
            services.AddScoped(typeof(IBookingService), typeof(BookingService));
            services.AddScoped(typeof(IWorkPlaceService), typeof(WorkPlaceService));
            services.AddScoped(typeof(IMapService), typeof(MapService));
            services.AddScoped(typeof(IOfficeService), typeof(OfficeService));
            services.AddScoped(typeof(IParkingPlaceService), typeof(ParkingPlaceService));
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            #endregion

            #region JWToken
            // Update the JWT settings from the settings
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        // указывает, будет ли валидироваться издатель при валидации токена
                        ValidateIssuer = true,
                        // будет ли валидироваться потребитель токена
                        ValidateAudience = true,
                        // будет ли валидироваться время существования
                        ValidateLifetime = true,
                        // установка ключа безопасности
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:secretKey"])),
                        // валидация ключа безопасности
                        ValidateIssuerSigningKey = true,
                        ClockSkew = TimeSpan.Zero,
                        RequireExpirationTime = false, // ToDo Update
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                    };
                });
            #endregion
        }
    }
}
