using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Zoomra.Domain.Entities;
using Zoomra.Domain.Interfaces;
using Zoomra.Domain.DTOS;
using Zoomra.Application.Helper;

namespace Zoomra.Application.Services
{
    public class AdminService : IAdminService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AdminService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<AdminHospitalDto>> CreateHospitalAsync(AddHospitalDto dto)
        {
            var exists = await _unitOfWork.Hospitals.Query.AnyAsync(h => h.LicenseNumber == dto.LicenseNumber);
            if (exists) return Result<AdminHospitalDto>.Failure("Hospital License already registered.");

            var hospital = _mapper.Map<Hospital>(dto);
            await _unitOfWork.Hospitals.AddAsync(hospital);
            await _unitOfWork.SaveChangesAsync();

            return Result<AdminHospitalDto>.Success(_mapper.Map<AdminHospitalDto>(hospital));
        }

        public async Task<Result<bool>> DeleteHospitalAsync(int hospitalId)
        {
            var hospital = await _unitOfWork.Hospitals.GetByIdAsync(hospitalId);
            if (hospital == null) return Result<bool>.Failure("Hospital not found.");

            _unitOfWork.Hospitals.DeleteAsync(hospital);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
            
        }

        public async Task<Result<IEnumerable<AdminHospitalDto>>> GetAllHospitalsAsync()
        {
            var hospitals = await _unitOfWork.Hospitals.GetAllAsync();
            return Result<IEnumerable<AdminHospitalDto>>.Success(_mapper.Map<IEnumerable<AdminHospitalDto>>(hospitals));
        }

        public async Task<Result<bool>> AddRewardAsync(AddRewardDto dto)
        {
            var reward = _mapper.Map<Reward>(dto);
            await _unitOfWork.Rewards.AddAsync(reward);
            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to add reward.");
        }
    }
}