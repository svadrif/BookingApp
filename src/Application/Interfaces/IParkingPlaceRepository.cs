using Domain.Entities;

namespace Application.Interfaces
{
    public interface IParkingPlaceRepository : IGenericRepository<ParkingPlace>, IPageable<ParkingPlace>
    {
    }
}
