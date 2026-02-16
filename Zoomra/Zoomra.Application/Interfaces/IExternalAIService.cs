using Zoomra.Application.Helper;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Interfaces
{
    public interface IExternalAIService
    {
        Task<Result<ShortageResponseDto>> GetShortagePredictionsAsync(string hospitalId = null);

        // هنا بنستخدم EmergencyMatchDto اللي هو الـ Request
        Task<Result<DonorMatchResponseDto>> MatchDonorsAsync(EmergencyMatchDto request);

        Task<Result<WasteAnalyticsDto>> GetWasteAnalyticsAsync(string hospitalId = null);
    }
}