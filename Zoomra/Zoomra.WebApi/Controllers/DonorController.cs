using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Zoomra.Application.Interfaces;

namespace Zoomra.WebApi.Controllers
{
    [Authorize] // حماية: مفيش حد يدخل غير بـ Token
    [ApiController]
    [Route("api/[controller]")]
    public class DonorController : ControllerBase
    {
        private readonly IDonorService _donorService;

        public DonorController(IDonorService donorService)
        {
            _donorService = donorService;
        }

        // هاتي الـ ID من التوكن مرة واحدة عشان نستخدمه في كل الدوال
        private string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)!;

        // 1. شاشة الـ Home (الإحصائيات)
        [HttpGet("dashboard")]
        public async Task<IActionResult> GetDashboard()
        {
            var result = await _donorService.GetDashboardDataAsync(UserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // 2. شاشة الـ Profile (تاريخ التبرعات)
        [HttpGet("history")]
        public async Task<IActionResult> GetHistory()
        {
            var result = await _donorService.GetDonationHistoryAsync(UserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // 3. شاشة الـ Rewards (ملخص النقاط والـ Badges)
        [HttpGet("rewards-summary")]
        public async Task<IActionResult> GetRewardsSummary()
        {
            var result = await _donorService.GetRewardsSummaryAsync(UserId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // 4. شاشة المتجر (كل المكافآت المتاحة)
        [HttpGet("available-rewards")]
        public async Task<IActionResult> GetAvailableRewards()
        {
            var result = await _donorService.GetAvailableRewardsAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // 5. فعل استبدال النقاط
        [HttpPost("redeem/{rewardId}")]
        public async Task<IActionResult> Redeem(int rewardId)
        {
            var result = await _donorService.RedeemRewardAsync(UserId, rewardId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // 6. شاشة الخريطة (المستشفيات)
        [HttpGet("nearby-centers")]
        public async Task<IActionResult> GetCenters()
        {
            var result = await _donorService.GetNearbyCentersAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // 7. شاشة حالات الطوارئ (Emergency Requests)
        [HttpGet("emergencies")]
        public async Task<IActionResult> GetEmergencies()
        {
            var result = await _donorService.GetActiveEmergenciesAsync();
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }
    }
}