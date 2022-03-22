using Domain.Entities;
using System.Text.RegularExpressions;

namespace Infrastructure.Validations;

public class OfficeValidation
{
    public static bool Validate(Office office)
    {
        //validate country
        var regex = new Regex(@"([a-z]*\s?[a-z]){3,}",RegexOptions.IgnoreCase);
        var match = regex.Match(office.Country);
        if (!match.Success)
        {
            return false;
        }
        //validate city
        regex = new Regex(@"([a-z]*\s?[a-z]){3,}",RegexOptions.IgnoreCase);
        match = regex.Match(office.City);
        if (!match.Success)
        {
            return false;
        }
        //validate address
        regex = new Regex(@"(([0-9a-z]*\s?[a-z])|([a-z0-9]*\s?[a-z0-9])){3,}",RegexOptions.IgnoreCase); //1 groove str, groove str 1
        match = regex.Match(office.Address);
        if (!match.Success)
        {
            return false;
        }
        //validate name
        regex = new Regex(@".+",RegexOptions.IgnoreCase);
        match = regex.Match(office.Name);
        if (!match.Success)
        {
            return false;
        }
        return true;
    }
}