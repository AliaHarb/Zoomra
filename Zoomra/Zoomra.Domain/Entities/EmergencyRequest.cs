using System;
using System.Collections.Generic;

namespace Zoomra.Domain.Entities
{
    public class EmergencyRequest
    {
        public int Id { get; set; }
        public int HospitalId { get; set; }
        public Hospital? Hospital { get; set; }

        public string BloodTypeNeeded { get; set; } = null!;
        public int UnitsNeeded { get; set; }
        public string UrgencyLevel { get; set; } = null!;
        public DateTime RequestTime { get; set; } = DateTime.UtcNow;
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string Status { get; set; } = "Open";

        public ICollection<EmergencyMatch> EmergencyMatches { get; set; } = new List<EmergencyMatch>();
    }
}