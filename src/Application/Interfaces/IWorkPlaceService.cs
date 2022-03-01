﻿using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWorkPlaceService 
    {
        Task<IEnumerable<WorkPlace>> GetAllAsync();
        Task<WorkPlace> GetByIdAsync(Guid Id);
        Task<WorkPlace> AddAsync(WorkPlace workPlace);
        Task<WorkPlace> UpdateAsync(WorkPlace workPlace);
        Task<bool> RemoveAsync(WorkPlace workPlace);
        Task<IEnumerable<WorkPlace>> SearchAsync(Guid MapId);
    }
}
