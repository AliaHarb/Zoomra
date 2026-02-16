using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zoomra.Domain.DTOS
{
    public class AdminHospitalDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LicenseNumber { get; set; } = null!;
        public string? City { get; set; }
        public string? ContactPhone { get; set; }
        public string AdminId { get; set; } = null!; // المعرف الخاص بالموظف المسؤول
    }
}
