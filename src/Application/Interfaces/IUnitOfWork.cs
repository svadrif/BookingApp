using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IAppUserRepository AppUsers { get; }    
        IBookingRepository Bookings { get; }
        IMapRepository Maps { get; }
        IOfficeRepository Offices { get; }
        IParkingPlaceRepository ParkingPlaces { get; }
        IVacationRepository Vacations { get; }
        IWorkPlaceRepository WorkPlaces { get; }
        Task <int> Complete();
    }
}
