using System.Collections.Generic;
using System.Threading.Tasks;
using Zoomra.Domain.DTOS;
using Zoomra.Application.Helper;

namespace Zoomra.Domain.Interfaces
{
    public interface IAdminService
    {
        
        Task<Result<AdminHospitalDto>> CreateHospitalAsync(AddHospitalDto dto);
        Task<Result<bool>> DeleteHospitalAsync(int hospitalId);
        Task<Result<IEnumerable<AdminHospitalDto>>> GetAllHospitalsAsync();
        Task<Result<bool>> AddRewardAsync(AddRewardDto dto);
    }
}