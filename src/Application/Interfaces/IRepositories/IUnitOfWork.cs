namespace Application.Interfaces.IRepositories
{
    public interface IUnitOfWork
    {
        IAppUserRepository AppUsers { get; }
        IBookingRepository Bookings { get; }
        IMapRepository Maps { get; }
        IOfficeRepository Offices { get; }
        IParkingPlaceRepository ParkingPlaces { get; }
        IVacationRepository Vacations { get; }
        IWorkPlaceRepository WorkPlaces { get; }

        Task<int> CompleteAsync(CancellationToken cancellationToken = new CancellationToken());
    }
}
