using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Domain.Entities;
using Application.Interfaces;

namespace Infrastructure.Services
{
    public class StateService : IStateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILoggerManager _logger;
        public StateService(IUnitOfWork unitOfWork, ILoggerManager logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task AddAsync(State state)
        {
            try
            {
                await _unitOfWork.States.AddAsync(state);
                await _unitOfWork.CompleteAsync();
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(AddAsync)} action {ex}");                
            }
        }

        public async Task<State> GetByUserIdAsync(Guid userId, bool tracking = false)
        {
            try
            {
                var state = await _unitOfWork.States.GetByUserIdAsync(userId, tracking);
                return state;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(GetByUserIdAsync)} action {ex}");
                return null;
            }
        }

        public async Task<State> UpdateAsync(State state)
        {
            try
            {
                _unitOfWork.States.Update(state);
                await _unitOfWork.CompleteAsync();
                return state;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(UpdateAsync)} action {ex}");
                return null;
            }
        }

        public async Task<bool> RemoveByUserIdAsync(Guid userId)
        {
            try
            {
                var state = await _unitOfWork.States.GetByUserIdAsync(userId);
                _unitOfWork.States.Remove(state);
                await _unitOfWork.CompleteAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogWarn($"Non correct values in the {nameof(RemoveByUserIdAsync)} action {ex}");
                return false;
            }
        }
    }
}
