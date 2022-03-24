﻿using Application.DTOs.WorkPlaceDTO;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.Pagination;
using AutoMapper;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class WorkPlaceService : IWorkPlaceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public WorkPlaceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Guid> AddAsync(AddWorkPlaceDTO workPlaceDTO)
        {
            WorkPlace workPlace = _mapper.Map<WorkPlace>(workPlaceDTO);
            await _unitOfWork.WorkPlaces.AddAsync(workPlace);
            await _unitOfWork.CompleteAsync();
            return workPlace.Id;
        }

        public async Task<PagedList<GetWorkPlaceDTO>> GetPagedAsync(PagedQueryBase query)
        {
            var workPlaces = await _unitOfWork.WorkPlaces.GetPagedAsync(query);
            var mapWorkPlaces = _mapper.Map<List<GetWorkPlaceDTO>>(workPlaces);
            var workPlacesDTO = new PagedList<GetWorkPlaceDTO>(mapWorkPlaces, workPlaces.TotalCount, workPlaces.CurrentPage, workPlaces.PageSize);
            return workPlacesDTO;
        }

        public async Task<GetWorkPlaceDTO> GetByIdAsync(Guid Id)
        {
            var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(Id);
            return _mapper.Map<GetWorkPlaceDTO>(workPlace);
        }
        public async Task<PagedList<GetWorkPlaceDTO>> GetByAttributesAsync(bool isNextToWindow, bool hasPc, bool hasMonitor, bool hasKeyboard,
            bool hasMouse, bool hasHeadset, PagedQueryBase query)
        {
            var workPlaces = await _unitOfWork.WorkPlaces.GetPagedByAttributesAsync(isNextToWindow,  hasPc,  hasMonitor,  hasKeyboard,
                 hasMouse,  hasHeadset,  query);
            var mapWorkPlaces = _mapper.Map<List<GetWorkPlaceDTO>>(workPlaces);
            var workPlacesDTO = new PagedList<GetWorkPlaceDTO>(mapWorkPlaces, workPlaces.TotalCount, workPlaces.CurrentPage, workPlaces.PageSize);
            return workPlacesDTO;
        }
        public async Task<bool> RemoveAsync(Guid Id)
        {
            var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(Id);
            if (workPlace == null)
                return false;

            _unitOfWork.WorkPlaces.Remove(workPlace);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<PagedList<GetWorkPlaceDTO>> SearchByMapIdAsync(Guid mapId, PagedQueryBase query)
        {
            var workPlaces = await _unitOfWork.WorkPlaces.GetPagedByMapIdAsync(mapId, query);
            var workPlaceMaps = _mapper.Map<List<GetWorkPlaceDTO>>(workPlaces);
            var workPlacesDTO = new PagedList<GetWorkPlaceDTO>(workPlaceMaps, workPlaces.TotalCount, workPlaces.CurrentPage, workPlaces.PageSize);
            return workPlacesDTO;
        }

        public async Task<GetWorkPlaceDTO> UpdateAsync(UpdateWorkPlaceDTO workPlaceDTO)
        {
            var workPlace = await _unitOfWork.WorkPlaces.GetByIdAsync(workPlaceDTO.Id);
            if (workPlace == null)
                return null;

            _mapper.Map(workPlaceDTO, workPlace);
            _unitOfWork.WorkPlaces.Update(workPlace);
            await _unitOfWork.CompleteAsync();
            return _mapper.Map<GetWorkPlaceDTO>(workPlace);
        }
    }
}
