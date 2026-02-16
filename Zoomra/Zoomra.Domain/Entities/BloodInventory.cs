using System;

namespace Zoomra.Domain.Entities
{
    public class BloodInventory
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        public string BloodType { get; set; } = null!;
        public int CurrentUnitsCount { get; set; }
        public DateTime LastUpdatedDate { get; set; } = DateTime.UtcNow;
    }
}