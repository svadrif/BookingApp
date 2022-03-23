using Application.Interfaces;
using Application.Interfaces.IRepositories;

namespace Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IApplicationDbContext _context;
        private readonly ILoggerManager _logger;
        public IAppUserRepository AppUsers { get; private set; }
        public IMapRepository Maps { get; private set; }
        public IOfficeRepository Offices { get; private set; }
        public IBookingRepository Bookings { get; private set; }
        public IParkingPlaceRepository ParkingPlaces { get; private set; }
        public IVacationRepository Vacations { get; private set; }
        public IWorkPlaceRepository WorkPlaces { get; private set; }
        public IStateRepository States { get; private set; }
        public IBookingHistoryRepository BookingHistories { get; private set; }

        public UnitOfWork(IApplicationDbContext context,
            IAppUserRepository appUsers,
            IMapRepository maps,
            IOfficeRepository offices,
            IBookingRepository bookings,
            IParkingPlaceRepository parkingPlaces,
            IVacationRepository vacations,
            IWorkPlaceRepository workPlaces,
            IStateRepository states,
            IBookingHistoryRepository bookingHistories,
            ILoggerManager logger)
        {
            _context = context;
            AppUsers = appUsers;
            Maps = maps;
            Offices = offices;
            Bookings = bookings;
            ParkingPlaces = parkingPlaces;
            Vacations = vacations;
            WorkPlaces = workPlaces;
            States = states;
            BookingHistories = bookingHistories;
            _logger = logger;
        }

        public async Task<int> CompleteAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                return await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(CompleteAsync)} action {ex}");
                return 0;
            }
        }
    }
}
