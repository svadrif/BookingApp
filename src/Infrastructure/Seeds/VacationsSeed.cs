using Domain.Entities;

namespace Infrastructure.Seeds
{
    public static class VacationsSeed
    {
        public static List<Vacation> GetVacations(List<AppUser> users)
        {
            var vacations = new List<Vacation>();
            foreach (var user in users)
            {
                vacations.Add(new Vacation()
                {
                    VacationStart = DateTime.Now.AddDays(1),
                    VacationEnd = DateTime.Now.AddDays(15),
                    UserId = user.Id
                });
            }
            return vacations;
        }
    }
}
