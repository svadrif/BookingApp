using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingPlaceController : ControllerBase
    {
        private static List<ParkingPlace> users = new List<ParkingPlace>
        {

            new ParkingPlace
            {

            },
            new ParkingPlace
            {

            }
        };

    }
}
