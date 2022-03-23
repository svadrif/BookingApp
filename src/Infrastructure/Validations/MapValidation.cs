using Domain.Entities;

namespace Infrastructure.Validations;

public class MapValidation
{
    public static bool Validate(Map map)
    {
        if (map.Floor < 0)
        {
            return false;
        }
        return true;
    }
}