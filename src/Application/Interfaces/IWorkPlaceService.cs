using Domain.Entities;

namespace Application.Interfaces
{
    public interface IWorkPlaceService : IDisposable
    {
        Task<IEnumerable<WorkPlace>> GetAll();
        Task<WorkPlace> GetById(Guid Id);
        Task<WorkPlace> Add(WorkPlace workPlace);
        Task<WorkPlace> Update(WorkPlace workPlace);
        Task<bool> Remove(WorkPlace workPlace);
        Task<IEnumerable<WorkPlace>> Search(Guid MapId);
    }
}
