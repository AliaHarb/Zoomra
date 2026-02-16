using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;

namespace Zoomra.WebApi.Controllers
{
    [Authorize(Roles = "Hospital")]
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        // GET: لعرض حالة المخزون للموظف أول ما يفتح الصفحة
        [HttpGet("get-stock/{hospitalId}")]
        public async Task<IActionResult> GetStock(int hospitalId)
        {
            var result = await _inventoryService.GetHospitalInventoryAsync(hospitalId);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        // POST: لتحديث المخزون (إضافة/سحب)
        [HttpPost("update-stock")]
        public async Task<IActionResult> UpdateStock([FromBody] UpdateInventoryDto dto)
        {
            var result = await _inventoryService.UpdateStockAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

        [HttpPost("confirm-donation")]
        public async Task<IActionResult> ConfirmDonation([FromBody] ConfirmDonationDto dto)
        {
            var result = await _inventoryService.ConfirmDonationAsync(dto);
            return result.IsSuccess ? Ok(result) : BadRequest(result);
        }

    }
}