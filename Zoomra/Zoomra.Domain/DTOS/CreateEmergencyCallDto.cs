using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class CreateEmergencyCallDto
    {
        public int HospitalId { get; set; }
        public string BloodType { get; set; } = null!;
        public string UrgencyLevel { get; set; } = null!; // High, Critical
        public string Message { get; set; } = null!; // "Case in operating room, need O- urgently!"
    }
}
