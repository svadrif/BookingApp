using Application.Extentions;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class WorkPlaceRepository : GenericRepository<WorkPlace>, IWorkPlaceRepository
    {
        public WorkPlaceRepository(ApplicationDbContext context) : base(context) { }

        public async Task<PagedList<WorkPlace>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            return await GetAll(tracking).ToPagedListAsync(query);
        }

        public async Task<PagedList<WorkPlace>> GetPagedByAttributesAsync(bool isNextToWindow, bool hasPc, bool hasMonitor, bool hasKeyboard,
                                                                    bool hasMouse, bool hasHeadset, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.IsNextToWindow == isNextToWindow
                                && x.HasPC == hasPc
                                && x.HasMonitor == hasMonitor
                                && x.HasKeyboard == hasKeyboard
                                && x.HasMouse == hasMouse
                                && x.HasHeadset == hasHeadset,
                                tracking)
                        .ToPagedListAsync(query);
        }

        public async Task<PagedList<WorkPlace>> GetPagedByMapIdAsync(Guid mapId, PagedQueryBase query, bool tracking = false)
        {
            return await Search(x => x.MapId == mapId,
                                tracking)
                        .ToPagedListAsync(query);
        }
    }
}
