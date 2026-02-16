using System;
using Zoomra.Domain.Entities;

namespace Zoomra.Domain.Entities
{
    public class RewardRedemption
    {
        public int Id { get; set; }

      
        public string DonorId { get; set; } = null!;
        public ApplicationUser? Donor { get; set; }

       
        public int RewardId { get; set; }
        public Reward? Reward { get; set; }

        
        public DateTime RedemptionDate { get; set; } = DateTime.UtcNow;

       
        public string? PromoCode { get; set; }
    }
}