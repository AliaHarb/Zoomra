using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Zoomra.Application.Helper;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Entities;
using Zoomra.Domain.Interfaces;

namespace Zoomra.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly INotificationService _notificationService; // تم إضافة السيرفيس هنا

        public InventoryService(
            IUnitOfWork unitOfWork,
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            INotificationService notificationService) // حقن السيرفيس في الـ Constructor
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _notificationService = notificationService;
        }

        public async Task<Result<bool>> UpdateStockAsync(UpdateInventoryDto dto)
        {
            var inventory = await _unitOfWork.BloodInventories.Query
                .FirstOrDefaultAsync(b => b.HospitalId == dto.HospitalId && b.BloodType == dto.BloodType);

            if (inventory == null)
            {
                if (dto.TransactionType == "Used")
                    return Result<bool>.Failure("Blood type not found in stock.");

                inventory = new BloodInventory
                {
                    HospitalId = dto.HospitalId,
                    BloodType = dto.BloodType,
                    CurrentUnitsCount = 0,
                    LastUpdatedDate = DateTime.UtcNow
                };
                await _unitOfWork.BloodInventories.AddAsync(inventory);
            }

            if (dto.TransactionType == "Collected")
                inventory.CurrentUnitsCount += dto.UnitsCount;
            else if (dto.TransactionType == "Used")
            {
                if (inventory.CurrentUnitsCount < dto.UnitsCount)
                    return Result<bool>.Failure("Requested quantity exceeds current stock level.");

                inventory.CurrentUnitsCount -= dto.UnitsCount;
            }

            inventory.LastUpdatedDate = DateTime.UtcNow;

            var transaction = new InventoryTransaction
            {
                HospitalId = dto.HospitalId,
                BloodType = dto.BloodType,
                UnitsCount = dto.UnitsCount,
                TransactionType = dto.TransactionType,
                TransactionDate = DateTime.UtcNow
            };

            await _unitOfWork.InventoryTransactions.AddAsync(transaction);

            var result = await _unitOfWork.SaveChangesAsync();
            return result > 0 ? Result<bool>.Success(true) : Result<bool>.Failure("Failed to update inventory.");
        }

        public async Task<Result<IEnumerable<BloodStockDto>>> GetHospitalInventoryAsync(int hospitalId)
        {
            var stocks = await _unitOfWork.BloodInventories.Query
                .Where(b => b.HospitalId == hospitalId)
                .ToListAsync();

            var dtos = _mapper.Map<IEnumerable<BloodStockDto>>(stocks);
            return Result<IEnumerable<BloodStockDto>>.Success(dtos);
        }

        public async Task<Result<bool>> ConfirmDonationAsync(ConfirmDonationDto dto)
        {
            // 1. Update Inventory
            var stockUpdateResult = await UpdateStockAsync(new UpdateInventoryDto
            {
                HospitalId = dto.HospitalId,
                BloodType = dto.BloodType,
                UnitsCount = dto.UnitsCount,
                TransactionType = "Collected"
            });

            if (!stockUpdateResult.IsSuccess) return Result<bool>.Failure("Failed to update inventory.");

            // 2. Update Donor Points and Status
            var donor = await _userManager.FindByNameAsync(dto.DonorId)
                        ?? await _userManager.FindByIdAsync(dto.DonorId);

            if (donor != null)
            {
                donor.RewardPoints += 50;
                donor.LastDonationDate = DateTime.UtcNow;

                var updateResult = await _userManager.UpdateAsync(donor);

                if (updateResult.Succeeded)
                {
                    // الحتة اللي كنتِ عاوزاها: إرسال إشعار شكر وتأكيد النقاط
                    await _notificationService.CreateEmergencyNotificationAsync(
                        dto.BloodType,
                        $"Thank you! 50 points added to your account for donating {dto.BloodType}."
                    );
                }
            }
            else
            {
                return Result<bool>.Failure("Donor not found in the system.");
            }

            // 3. Close Emergency Call if requested
            if (dto.EmergencyCallId.HasValue && dto.ShouldCloseCall)
            {
                var emergencyCall = await _unitOfWork.EmergencyRequests.GetByIdAsync(dto.EmergencyCallId.Value);
                if (emergencyCall != null)
                {
                    emergencyCall.Status = "Closed";
                }
            }

            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}