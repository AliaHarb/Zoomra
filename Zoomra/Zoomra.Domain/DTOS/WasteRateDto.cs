using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class WasteRateDto
    {
        public string HospitalId { get; set; }
        public string BloodType { get; set; }
        public double WasteRate { get; set; }
        public string Status { get; set; } // ACCEPTABLE, HIGH, etc.
    }
}
