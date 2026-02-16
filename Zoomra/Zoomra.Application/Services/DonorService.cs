using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Zoomra.Application.Helper;
using Zoomra.Application.Interfaces;
using Zoomra.Domain.DTOS;
using Zoomra.Domain.Entities;
using Zoomra.Domain.Interfaces;

namespace Zoomra.Application.Services
{
    public class DonorService : IDonorService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonorService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<Result<DonorDashboardDto>> GetDashboardDataAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Result<DonorDashboardDto>.Failure("User Not Found");

            return Result<DonorDashboardDto>.Success(new DonorDashboardDto
            {
                FullName = user.FullName,
                BloodType = user.BloodType,
                Points = user.RewardPoints,
                UnitsDonated = user.TotalDonationsCount,
                HelpedLives = user.TotalDonationsCount * 3,
                NextDonationDate = user.NextEligibleDonationDate?.ToString("yyyy-MM-dd") ?? "Eligible Now"
            });
        }

        public async Task<Result<bool>> RedeemRewardAsync(string userId, int rewardId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            // التصحيح: بنستخدم _unitOfWork.Rewards مباشرة بدون Repository<T>()
            var reward = await _unitOfWork.Rewards.GetByIdAsync(rewardId);

            if (reward == null || user == null) return Result<bool>.Failure("Data Error");
            if (user.RewardPoints < reward.PointsCost) return Result<bool>.Failure("Points not enough!");

            user.RewardPoints -= reward.PointsCost;
            var result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                var redemption = new RewardRedemption
                {
                    DonorId = userId,
                    RewardId = rewardId,
                    RedemptionDate = DateTime.UtcNow
                };

                // التصحيح: نستخدم الـ Property المعرفة في الـ UnitOfWork
                await _unitOfWork.RewardRedemptions.AddAsync(redemption);
                await _unitOfWork.SaveChangesAsync(); // مش CompleteAsync
                return Result<bool>.Success(true);
            }
            return Result<bool>.Failure("Redeem failed");
        }

        public async Task<Result<IEnumerable<RewardDto>>> GetAvailableRewardsAsync()
        {
            var rewards = await _unitOfWork.Rewards.GetAllAsync();
            return Result<IEnumerable<RewardDto>>.Success(_mapper.Map<IEnumerable<RewardDto>>(rewards));
        }

        public async Task<Result<IEnumerable<DonationHistoryDto>>> GetDonationHistoryAsync(string userId)
        {
            // بنستخدم الـ Donations مباشرة
            var donations = await _unitOfWork.Donations.GetAllAsync();
            var userDonations = donations.Where(x => x.DonorId == userId);
            return Result<IEnumerable<DonationHistoryDto>>.Success(_mapper.Map<IEnumerable<DonationHistoryDto>>(userDonations));
        }

        public async Task<Result<IEnumerable<DonationCenterDto>>> GetNearbyCentersAsync()
        {
            var centers = await _unitOfWork.Hospitals.GetAllAsync();
            return Result<IEnumerable<DonationCenterDto>>.Success(_mapper.Map<IEnumerable<DonationCenterDto>>(centers));
        }

        public async Task<Result<IEnumerable<EmergencyCallDto>>> GetActiveEmergenciesAsync()
        {
            // تأكدي لو عندك جدول اسمه EmergencyCalls أو استخدمي الـ Requests
            var calls = await _unitOfWork.EmergencyRequests.GetAllAsync();
            return Result<IEnumerable<EmergencyCallDto>>.Success(_mapper.Map<IEnumerable<EmergencyCallDto>>(calls));
        }

        public async Task<Result<UserRewardsSummaryDto>> GetRewardsSummaryAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return Result<UserRewardsSummaryDto>.Failure("Not Found");

            return Result<UserRewardsSummaryDto>.Success(new UserRewardsSummaryDto
            {
                CurrentPoints = user.RewardPoints,
                CurrentBadge = user.RewardPoints >= 1000 ? "Silver Donor" : "Bronze Donor",
                NextLevelPoints = 2000,
                Badges = new List<BadgeDto>
                {
                    new BadgeDto { Name = "Bronze", PointsRequired = 200, IsUnlocked = user.RewardPoints >= 200 },
                    new BadgeDto { Name = "Silver", PointsRequired = 1000, IsUnlocked = user.RewardPoints >= 1000 }
                }
            });
        }
    }
}