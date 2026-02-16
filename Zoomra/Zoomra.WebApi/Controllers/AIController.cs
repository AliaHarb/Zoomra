using Microsoft.AspNetCore.Mvc;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;

namespace Zoomra.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AIController : ControllerBase
    {
        private readonly IExternalAIService _aiService;

        public AIController(IExternalAIService aiService)
        {
            _aiService = aiService;
        }

        [HttpPost("predict-shortage")]
        public async Task<IActionResult> GetShortagePredictions([FromQuery] string? hospitalId)
        {
            var result = await _aiService.GetShortagePredictionsAsync(hospitalId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("match-donors")]
        public async Task<IActionResult> MatchDonors([FromBody] EmergencyMatchDto request)
        {
            var result = await _aiService.MatchDonorsAsync(request);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpGet("waste-analytics")]
        public async Task<IActionResult> GetWasteAnalytics([FromQuery] string? hospitalId)
        {
            var result = await _aiService.GetWasteAnalyticsAsync(hospitalId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}