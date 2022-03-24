﻿using Application.Extentions;
using Application.Interfaces;
using Application.Interfaces.IRepositories;
using Application.Pagination;
using Domain.Entities;
using Infrastructure.Context;

namespace Infrastructure.Repositories
{
    public class MapRepository : GenericRepository<Map>, IMapRepository
    {
        private readonly ILoggerManager _logger;
        public MapRepository(ApplicationDbContext context, ILoggerManager logger) : base(context)
        {
            _logger = logger;
        }

        public async Task<PagedList<Map>> GetPagedAsync(PagedQueryBase query, bool tracking = false)
        {
            try
            { 
            return await GetAll(tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedAsync)}action {ex}");
                return null;
            }
        }

        public async Task<PagedList<Map>> GetPagedByAttributesAsync(bool hasKitchen, bool hasConfRoom, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.HasKitchen == hasKitchen
                                && x.HasConfRoom == hasConfRoom,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByAttributesAsync)}action {ex}");
                return null;
            }
        }

        public async Task<PagedList<Map>> GetPagedByOfficeIdAsync(Guid officeId, PagedQueryBase query, bool tracking = false)
        {
            try
            {
                return await Search(x => x.OfficeId == officeId,
                                tracking)
                        .Sort(query.SortOn, query.SortDirection)
                        .ToPagedListAsync(query);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong in the {nameof(GetPagedByOfficeIdAsync)}action {ex}");
                return null;
            }
        }
    }
}
