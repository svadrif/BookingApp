using Domain.Entities;

namespace Infrastructure.Seeds
{
    public static class ParkingPlacesSeed
    {
        public static List<ParkingPlace> GetParkingPlaces(List<Office> offices)
        {
            var parkingPlaces = new List<ParkingPlace>();
            foreach (var office in offices)
            {
                parkingPlaces.AddRange(new[]
                {
                    new ParkingPlace
                    {
                        Number = "A1",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "A2",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "A3",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "B1",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "B2",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "C1",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "D1",
                        OfficeId = office.Id
                    },
                    new ParkingPlace
                    {
                        Number = "D2",
                        OfficeId = office.Id
                    },
                });
            }
            return parkingPlaces;
        }
    }
}
