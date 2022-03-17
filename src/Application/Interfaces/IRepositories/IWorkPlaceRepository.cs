using Application.Pagination;
using Domain.Entities;

namespace Application.Interfaces.IRepositories
{
    public interface IWorkPlaceRepository : IGenericRepository<WorkPlace>, IPageable<WorkPlace>
    {
        Task<PagedList<WorkPlace>> GetPagedByMapIdAsync(Guid mapId, PagedQueryBase query, bool tracking = false);
        Task<PagedList<WorkPlace>> GetPagedByAttributesAsync(bool isNextToWindow, bool hasPc, bool hasMonitor, bool hasKeyboard,
                                                             bool hasMouse, bool hasHeadset, PagedQueryBase query, bool tracking = false);
    }
}
