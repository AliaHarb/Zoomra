using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Zoomra.Domain.Entities 
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = null!;

        public string NationalId { get; set; } = null!;
        public string BloodType { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
 
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? City { get; set; }

        // 3.  (Digital Blood Card)
        public DateTime? LastDonationDate { get; set; }
        public DateTime? NextEligibleDonationDate { get; set; }
        public int TotalDonationsCount { get; set; } = 0;

        // 4. نظام النقاط  
        public int RewardPoints { get; set; } = 0;
        public bool IsEligibleToDonate { get; set; } = true;
        public bool IsAvailableForEmergency { get; set; } = true; 
        public string? DeviceToken { get; set; } 

        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<Donation> Donations { get; set; } = new List<Donation>();
        public ICollection<EmergencyMatch> EmergencyMatches { get; set; } = new List<EmergencyMatch>();
        public ICollection<RewardRedemption> RewardRedemptions { get; set; } = new List<RewardRedemption>();
        public ICollection<Notification> Notifications { get; set; } = new List<Notification>();
    }
}