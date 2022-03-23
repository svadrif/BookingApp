using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;
using Serilog;

namespace Infrastructure.Repositories
{
    public class WorkPlaceRepository : GenericRepository<WorkPlace>, IWorkPlaceRepository
    {
        public WorkPlaceRepository(ApplicationDbContext context, ILoggerManager logger) : base(context, logger) { }

        public async Task<PagedList<WorkPlace>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await GetAll(tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedAsync)} action {ex}");
                return new PagedList<WorkPlace>(new List<WorkPlace>(), 0, 0, 0);
            }
        }

        public async Task<PagedList<WorkPlace>> GetPagedByAttributesAsync(bool isNextToWindow, bool hasPc, bool hasMonitor, bool hasKeyboard,
                                                                    bool hasMouse, bool hasHeadset, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.IsNextToWindow == isNextToWindow
                                    && x.HasPC == hasPc
                                    && x.HasMonitor == hasMonitor
                                    && x.HasKeyboard == hasKeyboard
                                    && x.HasMouse == hasMouse
                                    && x.HasHeadset == hasHeadset,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByAttributesAsync)} action {ex}");
                return new PagedList<WorkPlace>(new List<WorkPlace>(), 0, 0, 0);
            }
        }

        public async Task<PagedList<WorkPlace>> GetPagedByMapIdAsync(Guid mapId, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.MapId == mapId,
                                    tracking)
                            .Sort(query.SortOn, query.SortDirection)
                            .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByMapIdAsync)} action {ex}");
                return new PagedList<WorkPlace>(new List<WorkPlace>(), 0, 0, 0);
            }
        }
    }
}
