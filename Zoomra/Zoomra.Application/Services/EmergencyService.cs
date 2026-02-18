    using AutoMapper;
    using global::Zoomra.Application.Helper;
    using global::Zoomra.Application.Interfaces;
    using global::Zoomra.Domain.DTOS;
    using global::Zoomra.Domain.Entities;
    using global::Zoomra.Domain.Interfaces;
    

namespace Zoomra.Application.Services
{
    public class EmergencyService : IEmergencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public EmergencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Result<bool>> RaiseEmergencyAsync(CreateEmergencyCallDto dto)
        {
            // 1. Map DTO to Entity
            var emergencyRequest = _mapper.Map<EmergencyRequest>(dto);

           
            emergencyRequest.RequestTime = DateTime.UtcNow;
            emergencyRequest.Status = "Open";

            // 2. Save to Database
            await _unitOfWork.EmergencyRequests.AddAsync(emergencyRequest);
            var result = await _unitOfWork.SaveChangesAsync();

            if (result > 0)
            {
                return Result<bool>.Success(true);
            }

            return Result<bool>.Failure("Failed to create emergency request.");
        }
    }
}
