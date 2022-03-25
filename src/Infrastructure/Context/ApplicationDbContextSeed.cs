using Application.Interfaces.IRepositories;
using Infrastructure.Repositories;
using Infrastructure.Seeds;

namespace Infrastructure.Context
{
    public class ApplicationDbContextSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await DemoData.AddUsersAsync(context);
            await DemoData.AddVacationsAsync(context);
            await DemoData.AddOfficesAsync(context);
            await DemoData.AddMapsAsync(context);
            await DemoData.AddWorkPlacesAsync(context);
            await DemoData.AddParkingPlacesAsync(context);
        }
    }
}
