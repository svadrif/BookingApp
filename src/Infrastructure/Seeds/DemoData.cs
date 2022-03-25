using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Seeds
{
    public static class DemoData
    {
        public static async Task AddUsersAsync(ApplicationDbContext context)
        {
            foreach (var user in UsersSeed.GetUsers())
            {
                if (context.AppUsers.Any(x => x.TelegramId == user.TelegramId))
                {
                    continue;
                }
                await context.AppUsers.AddAsync(user);
                await context.SaveChangesAsync();

                if (context.States.Any(x => x.UserId == user.Id))
                {
                    continue;
                }
                await context.States.AddAsync(new State { UserId = user.Id });
                await context.SaveChangesAsync();

                if (context.BookingHistories.Any(x => x.UserId == user.Id))
                {
                    continue;
                }
                await context.BookingHistories.AddAsync(new BookingHistory { UserId = user.Id });
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddVacationsAsync(ApplicationDbContext context)
        {
            AppUser varUser;
            foreach (var user in UsersSeed.GetUsers())
            {
                varUser = context.AppUsers.FirstOrDefault(x => x.TelegramId == user.TelegramId);
                if (varUser == null || context.Vacations.Any(x => x.UserId == varUser.Id))
                {
                    continue;
                }

                foreach (var vacation in VacationsSeed.GetVacations(new List<AppUser> { varUser }))
                {
                    await context.Vacations.AddAsync(vacation);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task AddOfficesAsync(ApplicationDbContext context)
        {
            foreach (var office in OfficesSeed.GetOffices())
            {
                if (context.Offices.Any(x => x.Address == office.Address))
                {
                    continue;
                }
                await context.Offices.AddAsync(office);
                await context.SaveChangesAsync();
            }
        }

        public static async Task AddMapsAsync(ApplicationDbContext context)
        {
            Office varOffice;
            foreach (var office in OfficesSeed.GetOffices())
            {
                varOffice = context.Offices.FirstOrDefault(x => x.Address == office.Address);
                if (varOffice == null || context.Maps.Any(x => x.OfficeId == varOffice.Id))
                {
                    continue;
                }
                foreach (var map in MapsSeed.GetMaps(new List<Office> { varOffice }))
                {
                    await context.Maps.AddAsync(map);
                    await context.SaveChangesAsync();
                }
            }
        }

        public static async Task AddWorkPlacesAsync(ApplicationDbContext context)
        {
            Office varOffice;
            Map varMap;
            foreach (var office in OfficesSeed.GetOffices())
            {
                varOffice = context.Offices.FirstOrDefault(x => x.Address == office.Address);
                if (varOffice == null || context.Maps.Any(x => x.OfficeId == varOffice.Id))
                {
                    continue;
                }
                foreach (var map in MapsSeed.GetMaps(new List<Office> { varOffice }))
                {
                    varMap = context.Maps.FirstOrDefault(x => x.OfficeId == varOffice.Id && x.Floor == map.Floor);
                    if (varMap == null || context.WorkPlaces.Any(x => x.MapId == map.Id))
                    {
                        continue;
                    }
                    foreach (var workPlace in WorkPlacesSeed.GetWorkPlaces(new List<Map> { varMap }))
                    {
                        await context.WorkPlaces.AddAsync(workPlace);
                        await context.SaveChangesAsync();
                    }
                }
            }
        }

        public static async Task AddParkingPlacesAsync(ApplicationDbContext context)
        {
            Office varOffice;
            foreach (var office in OfficesSeed.GetOffices())
            {
                varOffice = context.Offices.FirstOrDefault(x => x.Address == office.Address);
                if (varOffice == null || context.ParkingPlaces.Any(x => x.OfficeId == varOffice.Id))
                {
                    continue;
                }
                foreach (var parkingPlace in ParkingPlacesSeed.GetParkingPlaces(new List<Office> { varOffice }))
                {
                    await context.ParkingPlaces.AddAsync(parkingPlace);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}
