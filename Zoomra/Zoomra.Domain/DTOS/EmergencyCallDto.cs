using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class EmergencyCallDto
    {
        public int Id { get; set; }
        public string BloodType { get; set; } = null!;
        public string HospitalName { get; set; } = null!;
        public string Location { get; set; } = null!;
        public string UrgentLevel { get; set; } = null!; // Urgent, Critical
    }
}
