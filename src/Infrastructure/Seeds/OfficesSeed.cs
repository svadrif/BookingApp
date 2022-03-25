using Domain.Entities;

namespace Infrastructure.Seeds
{
    public static class OfficesSeed
    {
        public static List<Office> GetOffices()
        {
            return new List<Office>
            {
                new Office
                {
                    Country = "Belarus",
                    City = "Gomel",
                    Address = "Pushkin St. 2",
                    Name = "Exadel"
                },
                new Office
                {
                    Country = "Belarus",
                    City = "Minsk",
                    Address = "Naturalists St. 3",
                    Name = "Exadel"
                },
                new Office
                {
                    Country = "Belarus",
                    City = "Minsk",
                    Address = "Prytyckaha St. 156",
                    Name = "Exadel"
                },
                new Office
                {
                    Country = "Uzbekistan",
                    City = "Tashkent",
                    Address = "73 Mirzo Ulug'bek shoh ko'chasi",
                    Name = "Exadel"
                }
            };
        }
    }
}
