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

        public InventoryService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager; 
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

            // Log transaction for AI predictive intelligence
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
            // 1. تحديث المخزون (بنزود كيس الدم اللي اتبرع بيه)
            var stockUpdateResult = await UpdateStockAsync(new UpdateInventoryDto
            {
                HospitalId = dto.HospitalId,
                BloodType = dto.BloodType,
                UnitsCount = dto.UnitsCount,
                TransactionType = "Collected"
            });

            if (!stockUpdateResult.IsSuccess) return Result<bool>.Failure("Failed to update inventory.");

            // 2. إضافة النقاط للمتبرع وتحديث بياناته عن طريق UserManager
            // بنبحث بالـ UserName (اللي هو غالباً الرقم القومي) أو الـ ID
            var donor = await _userManager.FindByNameAsync(dto.DonorId)
                        ?? await _userManager.FindByIdAsync(dto.DonorId);

            if (donor != null)
            {
                donor.RewardPoints += 50; // مكافأة التبرع 
                donor.LastDonationDate = DateTime.UtcNow;

                // تحديث بيانات اليوزر في الـ Identity
                await _userManager.UpdateAsync(donor);
            }
            else
            {
                return Result<bool>.Failure("Donor not found in the system.");
            }

            // 3. قفل طلب الاستغاثة لو الموظف اختار "اكتفاء العجز"
            if (dto.EmergencyCallId.HasValue && dto.ShouldCloseCall)
            {
                var emergencyCall = await _unitOfWork.EmergencyRequests.GetByIdAsync(dto.EmergencyCallId.Value);
                if (emergencyCall != null)
                {
                    emergencyCall.Status = "Closed";
                }
            }

            // حفظ التغييرات الخاصة بـ EmergencyCall و Inventory
            var result = await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Success(true);
        }
    }
}