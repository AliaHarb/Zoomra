using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;
using Zoomra.Application.Helper;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;

namespace Zoomra.Application.Services
{
    public class ExternalAIService : IExternalAIService
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;

        public ExternalAIService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            // سحب اللينك من appsettings.json 
            _baseUrl = configuration["AISettings:BaseUrl"] ?? "https://blood-bank-api-abc123xyz-ew.a.run.app/api/v1/";
        }

        // 1. توقعات نقص المخزون (Shortage Prediction)
        public async Task<Result<ShortageResponseDto>> GetShortagePredictionsAsync(string hospitalId = null)
        {
            try
            {
                // بينادي على مسار التوقع المخصص في موديل الـ AI
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}predict/shortage", new { hospital_id = hospitalId });

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<ShortageResponseDto>();
                    return Result<ShortageResponseDto>.Success(data);
                }
                return Result<ShortageResponseDto>.Failure("AI Shortage Service is currently unavailable.");
            }
            catch (Exception ex)
            {
                return Result<ShortageResponseDto>.Failure($"AI Connection Error: {ex.Message}");
            }
        }

        // 2. مطابقة المتبرعين للطوارئ (Donor Matching)
        public async Task<Result<DonorMatchResponseDto>> MatchDonorsAsync(EmergencyMatchDto request)
        {
            try
            {
                // يرسل بيانات الحالة (الفصيلة والمكان) لموديل المطابقة
                var response = await _httpClient.PostAsJsonAsync($"{_baseUrl}match/emergency", request);

                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadFromJsonAsync<DonorMatchResponseDto>();
                    return Result<DonorMatchResponseDto>.Success(data);
                }
                return Result<DonorMatchResponseDto>.Failure("AI Matching Service failed to process request.");
            }
            catch (Exception ex)
            {
                return Result<DonorMatchResponseDto>.Failure($"Matching Service Error: {ex.Message}");
            }
        }

        // 3. تحليل الهدر (Waste Analytics)
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
                return Result<WasteAnalyticsDto>.Failure("Could not fetch AI Analytics.");
            }
            catch (Exception ex)
            {
                return Result<WasteAnalyticsDto>.Failure($"Analytics connection error: {ex.Message}");
            }
        }
    }
}