using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IParkingPlaceRepository : IGenericRepository<ParkingPlace>, IPageable<ParkingPlace>
    {
    }
}
