
using Domain.Entities;

namespace Infrastructure.Validations;

public class VacationValidation
{
    public static bool Validate(Vacation vacation)
    {
        if (vacation.VacationEnd.HasValue && vacation.VacationStart.HasValue)
        {
            var vacationEnd = vacation.VacationEnd??DateTimeOffset.UtcNow;
            var vacationStart = vacation.VacationStart??DateTimeOffset.UtcNow;
            if (DateTimeOffset.Compare(vacationEnd,vacationStart) < 0)
            {
                return false;
            }
        }    
        return true;
    }
}