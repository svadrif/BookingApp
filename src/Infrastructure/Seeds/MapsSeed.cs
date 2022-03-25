using Domain.Entities;

namespace Infrastructure.Seeds
{
    public static class MapsSeed
    {
        public static List<Map> GetMaps(List<Office> offices)
        {
            var maps = new List<Map>();
            foreach (var office in offices)
            {
                maps.AddRange(new[]
                {
                    new Map
                    {
                        Floor = 1,
                        HasKitchen = false,
                        HasConfRoom = false,
                        OfficeId = office.Id
                    },
                    new Map
                    {
                        Floor = 2,
                        HasKitchen = true,
                        HasConfRoom = false,
                        OfficeId = office.Id
                    },
                    new Map
                    {
                        Floor = 3,
                        HasKitchen = false,
                        HasConfRoom = true,
                        OfficeId = office.Id
                    },
                    new Map
                    {
                        Floor = 4,
                        HasKitchen = true,
                        HasConfRoom = true,
                        OfficeId = office.Id
                    },
                });
            }
            return maps;
        }
    }
}
