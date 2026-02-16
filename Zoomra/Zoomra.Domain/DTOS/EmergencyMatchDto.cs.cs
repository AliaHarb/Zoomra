using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class EmergencyMatchDto
    {
        public string HospitalId { get; set; } = null!;
        public string BloodTypeNeeded { get; set; } = null!;
        public int UnitsNeeded { get; set; }
        public LocationAI Location { get; set; } = new();
        public string UrgencyLevel { get; set; } = "CRITICAL"; // CRITICAL, HIGH, etc.
    }

    public class LocationAI
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
