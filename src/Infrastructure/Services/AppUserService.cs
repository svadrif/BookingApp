using Application.DTOs.AppUserDTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AppUserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddAppUserDTO appUserDTO)
        {
            AppUser appUser = _mapper.Map<AppUser>(appUserDTO);
            await _unitOfWork.AppUsers.AddAsync(appUser);
            await _unitOfWork.CompleteAsync();
            return appUser.Id;
        }

        public async Task<IEnumerable<GetAppUserDTO>> GetAllAsync()
        {
            var appUsers = _unitOfWork.AppUsers.GetAll(false);
            return _mapper.Map<IEnumerable<GetAppUserDTO>>(appUsers);
        }

        public async Task<GetAppUserDTO> GetByIdAsync(Guid Id)
        {
            var appUsers = await _unitOfWork.AppUsers.GetByIdAsync(Id);
            return _mapper.Map<GetAppUserDTO>(appUsers);
        }

        public async Task<bool> RemoveAsync(Guid Id)
        {
            var appUser = await _unitOfWork.AppUsers.GetByIdAsync(Id);
            if (appUser == null)
                return false;

            _unitOfWork.AppUsers.Remove(appUser);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<GetAppUserDTO> UpdateAsync(UpdateAppUserDTO appUserDTO)
        {
            var appUser = await _unitOfWork.AppUsers.GetByIdAsync(appUserDTO.Id);
            if (appUser == null)
                return null;

            _mapper.Map(appUserDTO, appUser);
            _unitOfWork.AppUsers.Update(appUser);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetAppUserDTO>(appUser);
        }
    }
}
