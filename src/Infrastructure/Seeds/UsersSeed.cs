using Domain.Entities;
using Domain.Enums;

namespace Infrastructure.Seeds
{
    public static class UsersSeed
    {
        public static List<AppUser> GetUsers()
        {
            return new List<AppUser>
            {
                new AppUser
                {
                    TelegramId = 718482458,
                    UserName = "Dmitry917",
                    FirstName = "Dmitry",
                    LastName = "Rohau",
                    TelephoneNumber = "+375259069376",
                    Email = "rogowdmitriy917@gmail.com",
                    Role = Roles.Admin,
                    EmploymentStart = DateTimeOffset.Now
                },
                new AppUser
                {
                    TelegramId = 860997183,
                    UserName = "ho_tema",
                    FirstName = "Artem",
                    LastName = "Hotko",
                    TelephoneNumber = "+375336164787",
                    Email = "hotko.artem2901@gmail.com",
                    Role = Roles.MapEditor,
                    EmploymentStart = DateTimeOffset.Now
                },
                new AppUser
                {
                    TelegramId = 298075991,
                    UserName = "Gwynbleidd05",
                    FirstName = "BXYND",
                    LastName = "",
                    TelephoneNumber = "+998915284101",
                    Email = "vagawww777@gmail.com",
                    Role = Roles.Manager,
                    EmploymentStart = DateTimeOffset.Now
                }
            };
        }
    }
}
