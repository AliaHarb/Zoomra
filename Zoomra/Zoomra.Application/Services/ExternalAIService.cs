using System.Net.Http.Json;
using Zoomra.Application.Helper;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Services
{
    public class ExternalAIService : IExternalAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl = "https://nondivergently-unmopped-gabriele.ngrok-free.dev/api/v1/";

        public ExternalAIService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // 1. توقعات نقص المخزون
        public async Task<Result<ShortageResponseDto>> GetShortagePredictionsAsync(string hospitalId = null)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}predict/shortage", new { hospital_id = hospitalId });
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ShortageResponseDto>();
                    return Result<ShortageResponseDto>.Success(data);
                }
                return Result<ShortageResponseDto>.Failure("AI Service returned an error.");
            }
            catch (Exception ex)
            {
                return Result<ShortageResponseDto>.Failure($"Connection failed: {ex.Message}");
            }
        }

        // 2. مطابقة المتبرعين للطوارئ - تم التعديل ليقبل EmergencyMatchDto
        // داخل كلاس ExternalAIService
        public async Task<Result<DonorMatchResponseDto>> MatchDonorsAsync(EmergencyMatchDto request)
        {
            try
            {
                // بنبعت الـ Request (اللي فيه اللوكيشن والفصيلة) للـ AI
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}match/emergency", request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<DonorMatchResponseDto>();
                    return Result<DonorMatchResponseDto>.Success(data);
                }
                return Result<DonorMatchResponseDto>.Failure("AI matching failed");
            }
            catch (Exception ex)
            {
                return Result<DonorMatchResponseDto>.Failure(ex.Message);
            }
        }

        // 3. تحليل الهدر (Analytics)
        public async Task<Result<WasteAnalyticsDto>> GetWasteAnalyticsAsync(string hospitalId = null)
        {
            try
            {
                var url = $"{_baseUrl}analytics/waste" + (!string.IsNullOrEmpty(hospitalId) ? $"?hospital_id={hospitalId}" : "");
                var response = await _httpClient.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<WasteAnalyticsDto>();
                    return Result<WasteAnalyticsDto>.Success(data);
                }
                return Result<WasteAnalyticsDto>.Failure("Could not fetch analytics.");
            }
            catch (Exception ex)
            {
                return Result<WasteAnalyticsDto>.Failure($"Analytics service error: {ex.Message}");
            }
        }
    }
}