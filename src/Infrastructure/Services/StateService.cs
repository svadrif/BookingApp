using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;

        public StateService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task AddAsync(State state)
        {
            await _unitOfWork.States.AddAsync(state);
            await _unitOfWork.CompleteAsync();
        }

        public async Task<State> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            var state = await _unitOfWork.States.GetByUserIdAsync(userId, tracking);
            return state;
        }

        public async Task<State> UpdateAsync(State state)
        {
            _unitOfWork.States.Update(state);
            await _unitOfWork.CompleteAsync();
            return state;
        }

        public async Task<bool> RemoveByUserIdAsync(Guid userId)
        {
            var state = await _unitOfWork.States.GetByUserIdAsync(userId);
            _unitOfWork.States.Remove(state);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}
