using System;
using Zoomra.Domain.Entities;

namespace Zoomra.Domain.Entities
{
    public class Donation
    {
        public int Id { get; set; }
        public string DonorId { get; set; } = null!;
        public ApplicationUser? Donor { get; set; }
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        public DateTime DonationDate { get; set; }
        public string BloodType { get; set; } = null!;
        public int UnitsDonated { get; set; }
        public string Status { get; set; } = null!;
        public int EarnedPoints { get; set; }
    }
}